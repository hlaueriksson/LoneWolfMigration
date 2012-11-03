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
    }
}
