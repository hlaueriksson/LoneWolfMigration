using System.Collections.Generic;
using System.IO;
using System.Linq;
using LoneWolf.Migration.Code;
using NUnit.Framework;
using Should;

namespace LoneWolf.Migration.Specs.Code
{
    public class Given_Generator
    {
        protected IGenerator generator;
        protected string[] input;
        protected IEnumerable<string> result;

        protected string[] GetFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public class Given_KaiDiscipline : Given_Generator
        {
            [SetUp]
            public void SetUp()
            {
                generator = new KaiDiscipline();
                input = GetFile(@"..\..\Data\section_with_KaiDiscipline_choice.xml");
                result = generator.Generate(input);
            }

            [Test]
            public void Then_the_choice_should_be_disabled_if_KaiDisciplineIsNotAcquired()
            {
                result.Count().ShouldEqual(1);
                result.First().ShouldEqual(".when(new KaiDisciplineIsNotAcquired(KaiDiscipline.SixthSense).then(new DisableChoice(\"2\")))");
            }
        }

        public class Given_RandomNumberTable : Given_Generator
        {
            [SetUp]
            public void SetUp()
            {
                generator = new RandomNumberTable();
                input = GetFile(@"..\..\Data\section_with_RandomNumber_choice.xml");
                result = generator.Generate(input);
            }

            [Test]
            public void Then_the_choice_should_be_disabled_if_RandomNumberIsNotBetween_specified_values()
            {
                result.Count().ShouldEqual(2);

                result.First().ShouldEqual(".when(new RandomNumberIsNotBetween(0, 4).then(new DisableChoice(\"2\")))");
                result.Last().ShouldEqual(".when(new RandomNumberIsNotBetween(5, 9).then(new DisableChoice(\"3\")))");
            }
        }

        public class Given_Combat : Given_Generator
        {
            [SetUp]
            public void SetUp()
            {
                generator = new Combat();
                input = GetFile(@"..\..\Data\section_with_Combat.xml");
                result = generator.Generate(input);
            }

            [Test]
            public void Then_the_enemy_should_be_added_to_the_combat()
            {
                result.Count().ShouldEqual(1);

                result.First().ShouldEqual(".set(new Combat().add(new Enemy(\"Foo\", 17, 25)))");
            }
        }

        public class Given_CombatSkill : Given_Generator
        {
            [SetUp]
            public void SetUp()
            {
                generator = new CombatSkill();
                input = GetFile(@"..\..\Data\section_with_CombatSkill_modifier.xml");
                result = generator.Generate(input);
            }

            [Test]
            public void Then_the_ModifyCombatSkill_should_be_added_to_the_combat()
            {
                result.Count().ShouldEqual(1);

                result.First().ShouldEqual(".add(new ModifyCombatSkill(2))");
            }
        }

        public class Given_GoldCrowns : Given_Generator
        {
            [SetUp]
            public void SetUp()
            {
                generator = new GoldCrowns();
                input = GetFile(@"..\..\Data\section_with_GoldCrowns.xml");
                result = generator.Generate(input);
            }

            [Test]
            public void Then_the_amout_of_GoldCrowns_should_be_added_to_the_section()
            {
                result.Count().ShouldEqual(1);

                result.First().ShouldEqual(".add(new GoldCrowns(28))");
            }
        }

        public class Given_Meal : Given_Generator
        {
            [SetUp]
            public void SetUp()
            {
                generator = new Meal();
                input = GetFile(@"..\..\Data\section_with_Meal.xml");
                result = generator.Generate(input);
            }

            [Test]
            public void Then_the_Meal_should_be_added_to_the_section()
            {
                result.Count().ShouldEqual(1);

                result.First().ShouldEqual(".add(new Meal(\"Food\"), TODO)");
            }
        }

        public class Given_Weapon : Given_Generator
        {
            [SetUp]
            public void SetUp()
            {
                generator = new Weapon();
                input = GetFile(@"..\..\Data\section_with_Weapons.xml");
                result = generator.Generate(input);
            }

            [Test]
            public void Then_the_Weapons_should_be_added_to_the_section()
            {
                result.Count().ShouldEqual(9);

                result.ElementAt(0).ShouldEqual(".add(new Weapon(\"Axe\"))");
                result.ElementAt(1).ShouldEqual(".add(new Weapon(\"Broadsword\"))");
                result.ElementAt(2).ShouldEqual(".add(new Weapon(\"Dagger\"))");
                result.ElementAt(3).ShouldEqual(".add(new Weapon(\"Mace\"))");
                result.ElementAt(4).ShouldEqual(".add(new Weapon(\"Quarterstaff\"))");
                result.ElementAt(5).ShouldEqual(".add(new Weapon(\"Short Sword\"))");
                result.ElementAt(6).ShouldEqual(".add(new Weapon(\"Spear\"))");
                result.ElementAt(7).ShouldEqual(".add(new Weapon(\"Sword\"))");
                result.ElementAt(8).ShouldEqual(".add(new Weapon(\"Warhammer\"))");
            }
        }
    }
}
