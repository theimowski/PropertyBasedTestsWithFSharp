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

##Verifying correctness

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

##Issues with standard approach

* Gives little confidence
* *Arrange* (*Fixture*) phase can be overwhelming

***

##Property based approach

***

##Working example

***

##How to come up with a good test

***

##How we use it in Phoenix

***