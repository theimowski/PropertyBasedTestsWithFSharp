- title : PBT with F#
- description : Property Based Testing with F#
- author : Tomasz Heimowski
- theme : night
- transition : default

***

##Property based testing with F#

###Plan

1. Standard approach
2. Property based approach
3. Working example
4. How to come up with a good test

Credits: http://fsharpforfunandprofit.com/

' DevDay - Mathias Brandewinder
' Phoenix knows
' Issues with UT - we encountered them

***

##Standard approach

###or "what's wrong with unit tests?"

---

##1. Correctness

--- 

*"Unit tests have been compared with shining a flashlight into a dark room in search of a monster. Shine the light into the room and then into all the scary corners. It doesn't mean the room is monster free - just that the monster isn't standing where you've shined your flashlight."*

![Monsters](images/capture.png)

---

    [<Fact>]
    let ``5 add 3 gives 8``() =
        let actual = add 5 3
        Assert.Equal(8, actual)

* How could add be implemented?

---

    [<Fact>]
    let ``isPalindrome returns true for "kajak"``() =
        let actual = isPalindrome "kajak"
        Assert.Equal(true, actual)

* How certain are we that the function is correctly implemented?
* How is null handled?
* How does it work for input with even count of letters?

---

*"Unit tests do not prove that a program runs correctly. 
Unit tests may at most tell that the program does not fail for specific cases."*

#### *"How many tests are enough?"*

---

##2. Arrange phase
####(also known as fixture)

---

    [<Fact>]
    let ``5 add 3 gives 8``() =
        // Arrange phase
        let x = 5
        let y = 3

        // Act phase
        let actual = add x y
        
        // Assert phase
        Assert.Equal(8, actual)


---

    [<Fact>]
    let ``customer is important if she has X subscriptions``() =
        // Arrange phase
        let inputXml = 
            """<customer>
                    <firstName>John</firstName>
                    <lastName>Doe</lastName>
                    <email>john.doe@example.com</com>
                    <subscriptions>
                        <subscription>
                            <type>...
                    ...
               </customer>
            """
        // Act phase
        let result = classifyCustomer inputXml

        // Assert phase
        Assert.True(result.IsImportantCustomer)

---

    [<Test>]
    let ``ignores disabled nuget feed from upstream`` () =
        // Arrange
        let upstream = 
            { NugetConfig.Empty with
                PackageSources = 
                    [ "MyGetDuality", ("https://www.myget.org/", None) ]
                    |> Map.ofList }
        let next = 
            // "Config.xml" is a file created for the arrange part
            NugetConfig.GetConfigNode (FileInfo "Config.xml") 
            |> Trial.returnOrFail
        let expected =
            { PackageSources = 
                [ "nuget.org", ("https://www.nuget.org/api/v2/",None) ]
                |> Map.ofList
              PackageRestoreEnabled = true
              PackageRestoreAutomatic = true }
        
        // Act
        let overridden = NugetConfig.OverrideConfig upstream next

        // Assert
        overridden |> shouldEqual expected

---

##Issues with standard approach
###(unit tests)

1. **Correctness** is not guaranteed
2. **Arrange** phase can be overwhelming

***

##Property based approach

---

##Idea

* Don't test for specific cases
* Think about what **properties** your code should have
* Test input / fixture is indeterministic - but it's always generated for a certain **type**
* We manipulate the generator, and the library makes sure to provide arbitrary instances

http://fsharpforfunandprofit.com/posts/property-based-testing/

---

##Building blocks

1. Generator
2. Shrinker

---

##Generator

* Library comes with predefined generators for primitive types and most basic language structures
* The mechanism is extensible, i.e. we can create our own generators for certain cases
* Given a generator and a seed, the library generates random instances of the desired type
* A single test is repeated multiple times, each time with a slightly "bigger" input

---

##Shrinker

* If for some input a test fails, the input is being shrunk
* If the shrunk input still causes the test to fail, it's being shrunk again
* Process is repeated until minimal faulty input is found

---

##Property based approach
###solving issues with unit tests

1. Arbitrary input can detect edge cases - **Correctness**
2. Much simpler and **consistent** test fixture - **Arrange**

***

##Working example

###(a journey to Property Based Test world)

---

###Revisited add - initial version

    [<Fact>]
    let ``5 add 3 gives 8``() =
        let actual = add 5 3
        Assert.Equal(8, actual)

---

###Add - parametrized tests

    [<Theory>]
    [<InlineData(5, 3, 8)>]
    [<InlineData(2, 2, 4)>]
    [<InlineData(-4, 9, 5)>]
    let ``add gives sum of two components``(x, y, expected) =
        let actual = add x y
        Assert.Equal(expected, actual)

---

###Add - using AutoFixture library

    [<Theory>]
    [<AutoData>]
    let ``add gives sum of two components``(x, y) =
        let expected = x + y
        let actual = add x y
        Assert.Equal(expected, actual)

* run only once?
* what about using the "+" operator?

---

###Add - using Property Based Approach

' 1. order doesn't matter - commutative property - przemienność
' 2. adding 0 to anything always gives the latter - identity property - element zerowy?
' 3. x + (y + z) = (x + y) + z - associative property - łączność

####Demo

***

##How to come up with a good test
#### applicable to a more complex problem

---

##Scott Wlaschin's list

1. "Different paths, same destination"
2. **"There and back again"**
3. "Some things never change"
4. **"The more things change, the more they stay the same"**
5. "Solve a smaller problem first"
6. "Hard to prove, easy to verify"
7. **"The test oracle"**

http://fsharpforfunandprofit.com/posts/property-based-testing-2/

---

## "There and back again"

    [<Property>]
    let ``parsing a stringified JSON gives original result`` (original: JSON) =
        let stringified = stringify original
        let parsed = parse stringified
        parsed = original

---

## "The more things change, the more they stay the same"

    [<Property>]
    let ``distinct is idemptotent`` (input: list<'a>) =
        let firstTurn = distinct input
        let secondTurn = distinct firstTurn
        firstTurn = secondTurn

---

## "The test oracle"
    
    [<Property>]
    let ``optimised version really works`` (input) =
        let optimisedResult = hiperFastConcurrentAlgorithm input
        let plainResult = simpleButSlowerAlgorithm input
        plainResult = optimisedResult

***

##Using Property Based Testing for testing XSLT
###Conclusions so far

* (+) Automatic detection of edge cases
* (+) Easier to maintain "Arrange" phase - everything is in Generator
* (+) All tests rely on the same Generator - improves consistency
* (+) Easy to find minimal faulty input thanks to Shrinker
* (-) "Assert" phase is slightly more complex
* (-) Error reasoning can sometimes get tricky

---

##Questions?

***