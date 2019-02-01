using System;
using System.Collections.Generic;

namespace CSharpFundamentals
{
    public static class VarianceTest
    {
        public static void Run()
        {
            Covariance();

            Contravariance();
        }

        private static void Covariance()
        {
            var animals = new List<Animal> {new Animal()};
            var cats = new List<Cat> {new Cat()};

            // ok
            BreatheAnimals(animals);

            // ok, what can be done with collection of animals can be also done with collection of cats - covariance
            BreatheAnimals(cats);

            // invalid, not all animals can meow
            // MeowCats(animals);
            
            // ok
            MeowCats(cats);

            var boxWithAnimal = new Box<Animal>(new Animal());
            var boxWithCat = new Box<Cat>(new Cat());
            UnpackAndBreathe(boxWithAnimal);
            UnpackAndBreathe(boxWithCat);
        }

        private static void Contravariance()
        {
            // ok
            PerformActionOnAnimal(BreatheAction);

            // invalid, not all animals can Meow - Action<Cat> is not assignable to Action<Animal>
            // PerformActionOnAnimal(MeowAction);

            // ok
            PerformActionOnCat(MeowAction);

            // ok, cat can breathe as all animals - contravariance Action<Animal> is assignable to Action<Cat>
            PerformActionOnCat(BreatheAction);
        }

        private static void PerformActionOnAnimal(Action<Animal> action) => action(new Animal());
        private static void PerformActionOnCat(Action<Cat> action) => action(new Cat());

        private static Action<Animal> BreatheAction => animal => animal.Breathe();
        private static Action<Cat> MeowAction => cat => cat.Meow();

        private static void BreatheAnimals(IEnumerable<Animal> animals)
        {
            foreach (var animal in animals)
            {
                animal.Breathe();
            }
        }

        private static void MeowCats(IEnumerable<Cat> cats)
        {
            foreach (var cat in cats)
            {
                cat.Meow();
            }
        }

        private class Animal
        {
            public virtual void Breathe() => Console.WriteLine("animal breathes");
        }

        private class Cat : Animal
        {
            public override void Breathe() => Console.WriteLine("cat breathes");
            public void Meow() => Console.WriteLine("cat meows");
        }

        private interface IBox<out T>
        {
            T Content { get; }
        }

        private class Box<T> : IBox<T>
        {
            public Box(T content)
            {
                Content = content;
            }

            public T Content { get; }
        }

        private static void UnpackAndBreathe(IBox<Animal> box)
        {
            Console.WriteLine($"Unpacking {box.Content}");
            box.Content.Breathe();
        }
    }
}