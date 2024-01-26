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
                Console.WriteLine("Failed to check name for Monster. Field \"name\" of class Monster have not such attribute");
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
            bool validationFailed = false;
            if(!ValidDaemonBrain(brain))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to create instance of Daemon class. Brain attribute failed");
                validationFailed = true;
            }
            if(!ValidMonsterName(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to create instance of Daemon class. Name attribute failed");
                validationFailed = true;
            }
            if (validationFailed)
            {
                Console.WriteLine($"Failed to create instance with brain \t{brain}\t and with name {name}\n");
                Console.ResetColor();
                return null;
            }
            else
                return new Mobs.Daemon(name, brain);
        }
        static Mobs.Daemon[] PrintDaemonsArray()
        {
            Mobs.Daemon[] daemon = new Mobs.Daemon[]
            {
                CreateDaemon(Mobs.Monster.MonsterTypes.ManAmogus.ToString(), 30),
                CreateDaemon(Mobs.Monster.MonsterTypes.BigGhoul.ToString(), 12),
                CreateDaemon(Mobs.Monster.MonsterTypes.SususAmogus.ToString(),7),
                CreateDaemon(Mobs.Monster.MonsterTypes.BigAmogus.ToString(),1),
                CreateDaemon(Mobs.Monster.MonsterTypes.ManGhoul.ToString(),5),
                CreateDaemon("DangerMonster",5),
                CreateDaemon("Monster",1),
                CreateDaemon("DangerMonsterAmogus",5),
            };
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
                if(DaemonArrayFailed(PrintDaemonsArray()))
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Не удалось создать все объекты");
                    Console.ResetColor();
                }
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
