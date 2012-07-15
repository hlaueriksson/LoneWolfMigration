using System.IO;
using System.Xml.Linq;
using LoneWolf.Migration.Core;
using NUnit.Framework;
using Should;

namespace LoneWolf.Migration.Specs.Core
{
    public class Given_Transformer : MockedSpec<Transformer>
    {
        public class When_Transform : Given_Transformer
        {
            private string result;
            private XDocument document;

            protected override void When()
            {
                result = ClassUnderTest.Transform(document);
            }

            protected XDocument GetXDocument(string path)
            {
                var text = File.ReadAllText(path).Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", string.Empty);

                return XDocument.Parse(text);
            }

            public class Section : When_Transform
            {
                protected override void Given()
                {
                    base.Given();
                    document = GetXDocument(@"..\..\Data\section.htm");
                }

                [Test]
                public void Then_the_root_element_should_be_section()
                {
                    result.ShouldStartWith("<section>");
                }

                [Test]
                public void Then_the_section_number_should_be_the_heading()
                {
                    result.ShouldContain("<h1>1</h1>");
                }

                [Test]
                public void Then_a_normal_paragraph_should_not_be_transformed()
                {
                    result.ShouldContain("<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>");
                }
            }

            public class Section_with_choice : When_Transform
            {
                protected override void Given()
                {
                    base.Given();
                    document = GetXDocument(@"..\..\Data\section_with_choice.htm");
                }

                [Test]
                public void Then_the_choice_paragraph_should_have_an_id()
                {
                    result.ShouldContain("<p class=\"choice\" id=\"2\">");
                }

                [Test]
                public void Then_the_choice_link_should_be_transformed_to_a_button()
                {
                    result.ShouldContain("<button type=\"button\" class=\"choice\" onclick=\"javascript:Section.turnTo(2);\">");
                }
            }

            public class Section_with_random_number_table : When_Transform
            {
                protected override void Given()
                {
                    base.Given();
                    document = GetXDocument(@"..\..\Data\section_with_random_number_table.htm");
                }

                [Test]
                public void Then_the_random_link_should_be_transformed_to_a_button()
                {
                    result.ShouldContain("<button type=\"button\" class=\"random-number\" onclick=\"javascript:Section.roll();\">");
                }
            }

            public class Section_with_two_random_number_tables : When_Transform
            {
                protected override void Given()
                {
                    base.Given();
                    document = GetXDocument(@"..\..\Data\section_with_two_random_number_tables.htm");
                }

                [Test]
                public void Then_the_buttons_onclick_javascript_call_should_have_a_index_parameter()
                {
                    result.ShouldContain("onclick=\"javascript:Section.roll(0);\"");
                    result.ShouldContain("onclick=\"javascript:Section.roll(1);\"");
                }
            }

            public class Section_with_combat : When_Transform
            {
                protected override void Given()
                {
                    base.Given();
                    document = GetXDocument(@"..\..\Data\section_with_combat.htm");
                }

                [Test]
                public void Then_the_combat_paragraph_should_be_transformed_to_a_button()
                {
                    result.ShouldContain("<button type=\"button\" class=\"combat\" onclick=\"javascript:Section.fight();\">");
                }
            }

            public class Section_with_two_combats : When_Transform
            {
                protected override void Given()
                {
                    base.Given();
                    document = GetXDocument(@"..\..\Data\section_with_two_combats.htm");
                }

                [Test]
                public void Then_the_buttons_onclick_javascript_call_should_have_a_index_parameter()
                {
                    result.ShouldContain("onclick=\"javascript:Section.fight(0);\"");
                    result.ShouldContain("onclick=\"javascript:Section.fight(1);\"");
                }
            }

            public class Section_with_illustration : When_Transform
            {
                protected override void Given()
                {
                    base.Given();
                    document = GetXDocument(@"..\..\Data\section_with_illustration.htm");
                }

                [Test]
                public void Then_the_illustration_div_should_be_transformed_to_a_figure()
                {
                    result.ShouldContain("<figure>");
                    result.ShouldContain("<img alt=\"\" src=\"ill1.png\" />");
                    result.ShouldContain("<figure>");
                }
            }

            public class Section_with_small_illustration : When_Transform
            {
                protected override void Given()
                {
                    base.Given();
                    document = GetXDocument(@"..\..\Data\section_with_small_illustration.htm");
                }

                [Test]
                public void Then_the_illustration_div_should_be_transformed_to_a_figure()
                {
                    result.ShouldContain("<figure>");
                    result.ShouldContain("<img alt=\"\" src=\"small1.png\" />");
                    result.ShouldContain("<figure>");
                }
            }

            public class Section_with_action_chart : When_Transform
            {
                protected override void Given()
                {
                    base.Given();
                    document = GetXDocument(@"..\..\Data\section_with_action_chart.htm");
                }

                [Test]
                public void Then_the_action_chart_link_should_be_transformed_to_a_button()
                {
                    result.ShouldContain("<button type=\"button\" class=\"action-chart\" onclick=\"javascript:Section.inventory();\">");
                }
            }
        }
    }
}
