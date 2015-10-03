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
5. How we use it in Phoenix

Credits: http://fsharpforfunandprofit.com/

***

##Standard approach

###Units tests

---

##1. Correctness

--- 

*"Unit tests have been compared with shining a flashlight into a dark room in search of a monster. Shine the light into the room and then into all the scary corners. It doesn't mean the room is monster free - just that the monster isn't standing where you've shined your flashlight."*

![Monsters](images/CAPTURE.png)

---

    [<Fact>]
    let ``5 add 3 gives 8``() =
        let actual = add 5 3
        Assert.Equal(8, actual)

---

*"Unit tests do not prove that a program runs correctly. 
Unit tests may at most tell that the program does not fail for specific cases."*

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

##Issues with standard approach
###(unit tests)

1. **Correctness** is not guaranteed
2. **Arrange** phase can be overwhelming

***

##Property based approach

***

##Working example

***

##How to come up with a good test

***

##How we use it in Phoenix

---

##Digital publishing

![pdf](images/pdf.png)

---

##Quark XPress

![qxp](images/QXP2015.png)

---

##Adobe InDesign

![indesign](images/indesign.png)

---

##Quark Stack

* Quark Publishing Platform
* Quark XML Author
* **Quark XPress Server**

---

##Quark XPress Server

![qxps](images/qxps.jpg)

###Quark XPress template + Modifier XML = PDF

***