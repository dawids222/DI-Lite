using DI_Lite;
using System;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Factory(() => "Tom");
            container.Scoped("tag", () => 21);
            container.Factory<IPet, Dog>();
            container.Single<Human>();
            container.Single<IHuman, Human>("");
            try
            {
                container.ThrowIfIsNotConstructable();
                var scope = container.CreateScope();
                var human = scope.Get<Human>();
                human.Greet();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    interface IPet { };
    interface IHuman { };

    record Human(string Name, int Age, IPet Pet) : IHuman
    {
        public void Greet() { System.Console.WriteLine($"Hello! My name is {Name}"); }
    }

    record Dog : IPet;
}
