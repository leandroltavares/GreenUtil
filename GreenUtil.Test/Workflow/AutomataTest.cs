using GreenUtil.Test.Dummy;
using GreenUtil.Test.Workflow.Validator;
using GreenUtil.Workflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.Workflow
{
    /// <summary>
    /// Test made to test the classes in workflow
    /// </summary>
    [TestClass]
    public class AutomataTest
    {
        private Automata<Foo> automata;

        private Foo container;

        private Foo ohterContainer;

        private string[] previousstates;

        private string[] nextstates;

        /// <summary>
        /// initialize all variables that will be used in our test(arrange)
        /// </summary>
        [TestInitialize]
        public void CreateBasicSetup()
        {
            previousstates = new string[] { "BEGIN", "MIDDLE", null };

            nextstates = new string[] { "BROKEN", "CANCELED", null };



            automata = new Automata<Foo>();
            automata.AddTransition("NEW", "VERIFIED", null);
            automata.AddTransition("VERIFIED", "READY", new FooValidator());
            automata.AddTransition(previousstates, nextstates, new FooValidator());

            ohterContainer = new Foo();
            ohterContainer.IntProp = 50;
            ohterContainer.DecimalProp = 2.71M;
            ohterContainer.StringProp = "This is false";

            container = new Foo();
            container.IntProp = 42;
            container.DecimalProp = 3.14M;
            container.StringProp = "This is a test";
        }

        /// <summary>
        /// method that test if true is returned if we use the right parameters in states
        /// </summary>
        /// <param name="previousState"></param>
        /// <param name="nextState"></param>
        [DataTestMethod]
        [DataRow("NEW", "VERIFIED")]
        [DataRow("BEGIN", "CANCELED")]
        [DataRow("MIDDLE", "CANCELED")]
        [DataRow("BEGIN", "BROKEN")]
        [DataRow(null, "BROKEN")]
        [DataRow(null, null)]
        [DataRow("BEGIN", null)]
        public void WhenTransitionIsValidThenEvaluateShouldReturnTrue(string previousState, string nextState)
        {
            //Act
            string outputMessage = string.Empty;
            var actual = automata.Evaluate(container, previousState, nextState, ref outputMessage);

            //Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(string.Empty, outputMessage);
        }

        /// <summary>
        /// method that test if false is returned if we use the wrong parameters in states
        /// </summary>
        /// <param name="previousState"></param>
        /// <param name="nextState"></param>
        [DataTestMethod]
        [DataRow("BEGIN", "VERIFIED")]
        [DataRow("NEW", "CANCELED")]
        [DataRow("CANCELED", "CANCELED")]
        [DataRow("CANCELED", "BROKEN")]
        public void WhenTransitionIsInvalidThenEvaluateShouldReturnFalse(string previousState, string nextState)
        {
            //Act
            string outputMessage = string.Empty;
            var actual = automata.Evaluate(container, previousState, nextState, ref outputMessage);

            //Assert
            Assert.IsFalse(actual);
            Assert.AreEqual($"O status {nextState} é inválido a partir do status {previousState}.", outputMessage);
        }
        /// <summary>
        /// method that test if false is returned if we use null parameters in states
        /// </summary>
        [TestMethod]
        public void WhenPreviousStateTransitionIsNullThenEvaluate()
        {
            //Act/Assert
            string outputMessage = string.Empty;
            Assert.IsFalse(automata.Evaluate(container, null, "SOME RANDOM STATE", ref outputMessage));

        }

        /// <summary>
        /// method that test if true is returned if we use null parameters in states
        /// </summary>
        [TestMethod]
        public void WhenNextStateTransitionIsNullThenEvaluate()
        {
            //Act/Assert
            string outputMessage = string.Empty;
            Assert.IsFalse(automata.Evaluate(container, "SOME RANDOM STATE", null, ref outputMessage));
        }


        /// <summary>
        /// method that test if true is returned if validator is not null and conditions are correspondents
        /// </summary>
        /// <param name="previousState"></param>
        /// <param name="nextState"></param>
        [DataTestMethod]
        [DataRow("VERIFIED", "READY")]
        [DataRow("BEGIN", "CANCELED")]
        [DataRow("MIDDLE", "CANCELED")]
        [DataRow("BEGIN", "BROKEN")]
        public void WhenValidatorIsNotNullAndConditionsAreValidThenEvaluateShouldReturnTrue(string previousState, string nextState)
        {
            //Act
            string outputMessage = string.Empty;
            bool actual = automata.Evaluate(container, previousState, nextState, ref outputMessage);

            //Assert
            Assert.IsTrue(actual);

        }

        /// <summary>
        ///  method that test if false is returned if validator is not null and conditions are not correspondents
        /// </summary>
        /// <param name="previousState"></param>
        /// <param name="nextState"></param>
        [DataTestMethod]
        [DataRow("VERIFIED", "READY")]
        [DataRow("BEGIN", "CANCELED")]
        [DataRow("MIDDLE", "CANCELED")]
        [DataRow("BEGIN", "BROKEN")]
        public void WhenValidatorIsNotNullAndConditionsAreWrongThenEvaluateShouldReturnFalse(string previousState, string nextState)
        {
            //Act
            string outputMessage = string.Empty;
            bool actual = automata.Evaluate(ohterContainer, previousState, nextState, ref outputMessage);

            //Assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// method that test if ArgumentsException is returned if we use try to add a duplicate transition 
        /// </summary>
        [TestMethod]
        public void WhenDuplicateTransitionAddedInDictionaryAndValidatorIsEqualThenAddTransitionShouldThrowException()
        {
            //Act/Assert
            Assert.ThrowsException<ArgumentException>(() => automata.AddTransition("VERIFIED", "READY", new FooValidator()));
        }

        /// <summary>
        /// method that test if ArgumentsException is returned if we use try to add a duplicate transition 
        /// </summary>
        [TestMethod]
        public void WhenDuplicateTransitionAddedInDictionaryAndValidatorIsDifferentThenAddTransitionShouldThrowException()
        {
            //Act/Assert a
            Assert.ThrowsException<ArgumentException>(() => automata.AddTransition("VERIFIED", "READY", null));
        }

        [TestMethod]
        public void WhenBothPreviousAndNextStatesAreEqualThenEqualsShouldReturnTrue()
        {
            //Arrange
            var transition1 = new Transition<string>("OLD", "NEW", null);
            var transition2 = new Transition<string>("OLD", "NEW", null);

            //Act
            var equal = transition1.Equals(transition2);

            Assert.IsTrue(equal);

        }


        [TestMethod]
        public void WhenPreviousStatesAreDifferentThenEqualsShouldReturnFalse()
        {
            //Arrange
            var transition1 = new Transition<string>("OLD", "NEW", null);
            var transition2 = new Transition<string>("OLD_DIFF", "NEW", null);

            //Act
            var equal = transition1.Equals(transition2);

            Assert.IsFalse(equal);
        }

        [TestMethod]
        public void WhenNextStatesAreDifferentThenEqualsShouldReturnFalse()
        {
            //Arrange
            var transition1 = new Transition<string>("OLD", "NEW", null);
            var transition2 = new Transition<string>("OLD", "NEW_DIFF", null);

            //Act
            var equal = transition1.Equals(transition2);

            Assert.IsFalse(equal);
        }

        [TestMethod]
        public void WhenOtherTransitionIsNullThenEqualsReturnFalse()
        {
            //Arrange
            var transition1 = new Transition<string>("OLD", "NEW", null);

            //Act
            var equal = transition1.Equals(null);

            Assert.IsFalse(equal);
        }
    }
}