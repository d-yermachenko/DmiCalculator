using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcReflectionUnitTests {

    public class TestFirstLevelObject {

        public TestSecondLevelObject FirstLevelPropertyOne { get; set; }

        public int IntProperty { get; set; }

        public string StringProperty { get; set; }


        public decimal DecimalProperty { get; set; }

        public TestSecondLevelObject FirstLevelPropertyTwo { get; set; }


    }

    public class TestSecondLevelObject {
        public TestThirdLevelObject SecondLevelPropertyOne { get; set; }

        public int IntProperty { get; set; }

        public string StringProperty { get; set; }

        public decimal DecimalProperty { get; set; }

        public TestThirdLevelObject SecondLevelPropertyTwo { get; set; }
    }

    public class TestThirdLevelObject {
        public int MyIntegerProperty { get; set; }

        public string MyStringPrioperty { get; set; }

        public decimal MyDecimalProperty { get; set; }

        public double MyFloatingPointValue { get; set; }

        public bool MyBooleanValue { get; set; }

    }

    public class TestObjectsGenerators {
        public static TestFirstLevelObject GenerateObject() {
            TestFirstLevelObject testFirst = new TestFirstLevelObject() {
                FirstLevelPropertyOne = new TestSecondLevelObject() {
                    DecimalProperty = 1210.1985M,
                    IntProperty = 48,
                    StringProperty = "FirstLevelPropertyOne",
                    SecondLevelPropertyOne = new TestThirdLevelObject() {
                        MyBooleanValue = true,
                        MyDecimalProperty = 1803.1961M,
                        MyFloatingPointValue = 14.4,
                        MyStringPrioperty = "My string property",
                        MyIntegerProperty = 186
                    },
                    SecondLevelPropertyTwo = new TestThirdLevelObject() {
                        MyBooleanValue = true,
                        MyDecimalProperty = 0507.1993M,
                        MyFloatingPointValue = 17.2,
                        MyStringPrioperty = "My string property",
                        MyIntegerProperty = 123
                    }

                },
                StringProperty = "String property",
                DecimalProperty = 158.247M,
                IntProperty = 857,
                FirstLevelPropertyTwo = new TestSecondLevelObject() {
                    DecimalProperty = 18.258M,
                    IntProperty = 85741,
                    StringProperty = "Second level property",
                    SecondLevelPropertyOne = new TestThirdLevelObject() {
                        MyBooleanValue = false,
                        MyFloatingPointValue = 5278.69,
                        MyDecimalProperty = 7854.258M,
                        MyStringPrioperty = "Another string property",
                        MyIntegerProperty = 14588
                    },
                    SecondLevelPropertyTwo = new TestThirdLevelObject() {
                        MyBooleanValue = true,
                        MyFloatingPointValue = 857.54,
                        MyDecimalProperty = 857414.85632M,
                        MyIntegerProperty = 147,
                        MyStringPrioperty = "Second on second level property"
                    }

                }
            };

            return testFirst;
        }

    }


}
