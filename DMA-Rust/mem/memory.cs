using wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using DMA_Rust.Rust.Classes;
using DMA_Rust.Rust;
using System.IO;

namespace DMA_Rust.mem
{
    public static class memory
    {
        public static uint _pid = 0;
        //public static Vmm vmm = new Vmm("-printf", "-v", "-device", "fpga");
        public static Vmm.MAP_MODULEENTRY GameAssembly;
        public static Thread UpdateThread_;
        public static Thread EntityListThread_;
        private static Vmm vmm;
        public static Form1 formMain;
        public static bool IsDMAConnected = false;

        public static void StartUp()
        {

            if (!File.Exists("mmap.txt"))
            {
                vmm = new Vmm("-printf", "-v", "-device", "fpga", "-waitinitialize");
                GetMemMap();
            }
            else
            {
                vmm = new Vmm("-printf", "-v", "-device", "fpga", "-memmap", "mmap.txt");
                IsDMAConnected = true;
            }



            Thread.Sleep(2000);

            if (!vmm.PidGetFromName("RustClient.exe", out _pid))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed To Find Rust PID..");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green; // Set color to Green
                Console.WriteLine("[Success] Found Rust PID at:  " + _pid);
                Console.ResetColor();
            }




            if (_pid != 0) {

                if (FixCr3()){

                    BuildClasses();
                }
            }


            void BuildClasses()
            {
                if (BasePlayer.BuildBasePlayer() && Playermodel.BuildPlayerModel() && Tod_sky.BuildTOD() && ConvarGraphics.getConvarGraphics() == true)
                {
                    UpdateThread_ = new Thread(UpdateThread.UTBegin);
                    UpdateThread_.Start();

                }  
            }



            bool FixCr3()
            {
                GameAssembly = vmm.Map_GetModuleFromName(_pid, "GameAssembly.dll");

                if (GameAssembly.vaBase != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Set color to Green

                    Console.WriteLine("[Success] Found GameAssembly.dll at:   0x" + GameAssembly.vaBase.ToString("X"));
                    Console.ResetColor();
                    return true;
                }
                vmm.InitializePlugins();



                while (true)
                {
                    byte[] bytes = new byte[4];
                    uint i = 0;

                    ulong nt = vmm.VfsRead("\\misc\\procinfo\\progress_percent.txt", 3, i, out bytes);

                    string fileContent = System.Text.Encoding.Default.GetString(bytes);
                    if (int.TryParse(fileContent, out int result) && result == 100)
                    {
                        break;
                    }




                }

                List<string> possibleDtbs = new List<string>();
                List<string> allParts = new List<string>();

                try
                {
                    byte[] bytes;
                    ulong dtbDataBytes = vmm.VfsRead("\\misc\\procinfo\\dtb.txt", 32768, 0, out bytes);
                    string dtbData = System.Text.Encoding.UTF8.GetString(bytes);


                    string[] lines = dtbData.Split('\n');
                    foreach (string line in lines)
                    {

                        string[] parts = line.Split();
                        if (parts.Length >= 5)
                        {
                            string a = string.Join(",", parts);
                            allParts.Add(a);
                        }

                    }
                    for (int i = 0; i < allParts.Count; i++)
                    {
                        allParts[i] = Regex.Replace(allParts[i], ",+", ",");
                    }

                    foreach (string part in allParts)
                    {
                        string[] items = part.Split(',');
                        string index = items[0];
                        string pid = items[1];
                        string dtb = items[2];
                        string kerneladdr = items[3];
                        string name = items[4];
                        int pidd = int.Parse(pid);
                        if (pidd == 0 | pidd == _pid)
                        {
                            possibleDtbs.Add(dtb);
                        }
                    }
                    foreach (string dtb in possibleDtbs)
                    {
                        ulong dtbValue;

                        ulong.TryParse(dtb, System.Globalization.NumberStyles.HexNumber, null, out dtbValue);

                        vmm.ConfigSet(Vmm.OPT_PROCESS_DTB | _pid, dtbValue);
                        try
                        {
                            GameAssembly = vmm.Map_GetModuleFromName(_pid, "GameAssembly.dll");
                            Console.WriteLine("GameAss" + GameAssembly.vaBase);
                            return true;
                        }
                        catch { }
                    }


                }
                catch { }
                Console.WriteLine("Failed To Patch DTB");
                return false;
            }

        }

        private static void GetMemMap()
        {
            try
            {
                var map = vmm.Map_GetPhysMem();
                if (map.Length == 0) throw new Exception("Map_GetPhysMem() returned no entries!");
                var sb = new StringBuilder();
                for (int i = 0; i < map.Length; i++)
                {
                    sb.AppendLine($"{i.ToString("D4")}  {map[i].pa.ToString("x")}  -  {(map[i].pa + map[i].cb - 1).ToString("x")}  ->  {map[i].pa.ToString("x")}");
                }
                File.WriteAllText("mmap.txt", sb.ToString());
            }
            catch (Exception ex)
            {
               Console.WriteLine("Unable to get MMap" +ex.ToString());
            }
        }


        #region Read/Write

        public static T ReadMemory<T>(ulong address)
        {
            if (address != 0)
            {
                uint size = (uint)Marshal.SizeOf(typeof(T));
                byte[] buffer = vmm.MemRead(_pid, address, size);
                T result = default(T);
                result = BytesToStructure<T>(buffer);
                return result;
            }
            else
            {
                return default(T);
            }
        }

        public static T BytesToStructure<T>(byte[] buffer)
        {
            T result = default(T);
            int size = buffer.Length;
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(buffer, 0, ptr, size);
            result = (T)Marshal.PtrToStructure(ptr, result.GetType());

            Marshal.FreeHGlobal(ptr);
            return result;
        }




        public static void WriteMemory<T>(ulong address, T value)
        {
            if (address != 0)
            {
                byte[] buffer = StructureToBytes<T>(value);
                vmm.MemWrite(_pid, address, buffer);
            }
        }

        public static byte[] StructureToBytes<T>(T structure)
        {
            int size = Marshal.SizeOf(structure);
            byte[] buffer = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structure, ptr, true);
            Marshal.Copy(ptr, buffer, 0, size);
            Marshal.FreeHGlobal(ptr);
            return buffer;
        }



        public static bool ReadUnicode(ulong address, int length, out string value)
        {
            value = null;
            if (vmm.ReadUtfUnicode8Memory(address, 2 * (uint)length, out var buffer))
            {
                string text = new string(Encoding.Unicode.GetChars(buffer));
                value = text.Split(default(char))[0];
                return true;
            }

            return false;
        }




        #region CopyPasta

        public static byte[] ReadStrBytes(ulong address, int size)
        {

            var buffer = new byte[size];

            var buf = vmm.MemRead(_pid, address, (uint)size, Vmm.FLAG_NOCACHE);
            buffer = buf.ToArray();

            return buffer;
        }
        public static T Median<T>(this IEnumerable<T> items)
        {
            var i = (int)Math.Ceiling((double)(items.Count() - 1) / 2);
            if (i >= 0)
            {
                var values = items.ToList();
                values.Sort();
                return values[i];
            }

            return default(T);
        }


        public static bool IsValidPointer(ulong address)
        {
            if (address >= 0x1000000 && address < 0x7FFFFFFFFFF)

            {
                return true;
            }
            return false;
        }

        public static T ReadNormandy<T>(ulong address, bool reportValidation = true, bool validatePointer = true)
        {
            try
            {
                var size = Marshal.SizeOf(typeof(T));
                var buffer = new byte[size];


                buffer = ReadStrBytes(address, size);
                var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                var data = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                handle.Free();

                return data;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static T ReadChain<T>(ulong address, uint[] offsets, bool reportValidation = true)
        {
            try
            {
                var size = Marshal.SizeOf(typeof(T));
                var buffer = new byte[size];


                for (int i = 0; i < offsets.Length - 1; i++)
                {

                    address += offsets[i];
                    address = ReadNormandy<ulong>(address);

                    if (IsValidPointer(address) == false)
                    {
                        return default(T);
                    }
                }

                buffer = ReadStrBytes(address + offsets[offsets.Length - 1], size);
                var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                var data = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                handle.Free();

                return data;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        #endregion

        #endregion


    }
}
