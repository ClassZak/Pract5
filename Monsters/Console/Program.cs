using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Mobs;
using System.Data;

namespace ConsoleApp
{
    internal class Program
    {
        #region код проверки объектов на допустимость
        static bool ValidDaemonBrain(int brain)
        {
            try 
            {
                object[] attributes = typeof(Mobs.Daemon).GetCustomAttributes(false);
                foreach (object attribute in attributes)
                {
                    if(attribute.GetType()==typeof(Mobs.DaemonBrainAttribute))
                    {
                        return brain >= ((Mobs.DaemonBrainAttribute)(attribute)).brain;
                    }
                }
                throw new Exception("Failed to check brain for Daemon. Daemon class have not such attribute");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }

            return false;
        }
        static bool ValidMonsterName(string name)
        {
            bool isValud = false;
            try
            {
                object[] attrs = (typeof(Mobs.Monster).GetField("name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetCustomAttributes(false));
                foreach (var item in attrs)
                {
                    if (item.GetType() == typeof(Mobs.MonsterNameAttribute))
                    {
                        foreach(string Name in ((Mobs.MonsterNameAttribute)(item)).names)
                        if(name.EndsWith(Name))
                        {
                            isValud = true;
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Не удалось сделать проверку имени:");
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }

            return isValud;
        }
        static bool DaemonArrayFailed(Mobs.Daemon[] daemon)
        {
            for (int i = 0; i < daemon.Length; ++i)
                if (daemon[i] == null)
                    return true;
            return false;
        }
        #endregion
        #region код объявления объектов
        static Mobs.Daemon CreateDaemon(string name,int brain)
        {
            if(!ValidDaemonBrain(brain))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to create instance of Daemon class. Brain attribute failed");
                Console.ResetColor();
                return null;

            }
                
            return new Mobs.Daemon(name, brain);
        }
        static Mobs.Daemon[] PrintDaemonsArray()
        {
            Mobs.Daemon[] daemon = new Mobs.Daemon[] { CreateDaemon("Sus", 7), CreateDaemon("Amogus", 150), CreateDaemon("AmogusZilla", 5) };
            Console.WriteLine($"Массив демонов({daemon.Length}):");
            for (int i = 0; i < daemon.Length; ++i)
            {

                Console.WriteLine($"Daemon\t{i}:");
                if (daemon[i] == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("NULL");
                    Console.ResetColor();
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(daemon[i].Name);
                Console.ResetColor();
            }
            return daemon;
        }
        #endregion
        
        static void Main(string[] args)
        {
            try
            {
                object[] attrs = 
                    typeof(Mobs.Monster).
                    GetField("name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetCustomAttributes(false);
                for(int i = 0;i< attrs.Length; ++i)
                {
                    Console.WriteLine($"{i}\t{attrs[i].ToString()}");
                }

                /*
                if(DaemonArrayFailed(PrintDaemonsArray()))
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Не удалось создать все объекты");
                    Console.ResetColor();
                }*/
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Исключение в главной процедуре!");
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            Console.ReadKey();
        }
    }
}
