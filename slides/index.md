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

###or "what's wrong with unit tests?"

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
3.

***

##Property based approach

***

##Working example

***

##How to come up with a good test

***

##How we use it in Phoenix

---

##Desktop publishing

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
* Quark XPress + Quark XPress **Server**

---

##Quark XPress Server

![qxps](images/qxps.jpg)

###Quark XPress template + Modifier XML = PDF

---

##Desktop publishing

![pdf](images/pdf.png)

---

##Desktop publishing automation

![automation](images/factory.png)

---

##Modifier XML

    [lang=xml]
    <LAYOUT>
        <PAGESEQUENCE MASTERREFERENCE="Main">
            <STORY>
                <PARAGRAPH PARASTYLE="m_header_1">
                    <RICHTEXT>My first publication</RICHTEXT>
                </PARAGRAPH>
                <PARAGRAPH PARASTYLE="m_body">
                    <RICHTEXT>Hello </RICHTEXT>
                    <RICHTEXT BOLD="TRUE">world!</RICHTEXT>
                </PARAGRAPH>
            </STORY>
        </PAGESEQUENCE>
    </LAYOUT>

---

##Dita XML
####(or rather Phoenix-Dita XML)

    [lang=xml]
    <topic>
        <title>My first publication</title>
        <body>
            <p>Hello <b>world!</b></p>
        </body>
    </topic>

---

##XSLT !!!

![epic](images/epic.jpg)

--- 

##XSLT can get as complex ...

![complex_xslt](images/complex_xslt.jpg)

---

##... as JavaScript ...

![js_pyramid](images/js_pyramid.jpg)

---

###... so it's good to have some tests

![need_tests](images/need_tests.jpg)

---

##Generator

    let title = gen {
        let! contents = contents
        return XElement("title", contents)
    }

    let body = gen {
        let! items = Gen.oneOf [ para; table; chart ] |> Gen.listOf
        return XElement("body", items)
    }

    let topic = gen {
        let! title = title
        let! body = body
        return XElement("topic", title, body)
    }

---

##Tests

    let schema = XmlSchema.Parse "Modifier.xsd"

    [<Property>]
    let ``modifier XML conforms to schema`` (topic: XDocument) =
        let output = xsltTransform "topic.xslt" topic
        doesNotThrow (fun () -> schema.Validate output)

* alternative -> schema-aware XSLT processor 

---

##Moar tests

    [<Property>]
    let ``if text node under "b" element then richtext has bold`` (topic) =
        let output = xsltTransform "topic.xslt" topic
        let textNodes = topic  |> xpath "//text()"
        let richtexts = output |> xpath "//RICHTEXT"
        
        (textNodes, richtexts)
        ||> Seq.zip
        |> Seq.filter (fst >> xpath "ancestor::b")
        |> Seq.forAll (snd >> xpath "@BOLD = 'TRUE'")

---

##Shrinker

    [lang=xml]
    <topic>
        <title>My first publication</title>
        <body>
            <image href="unicorn.pdf">
            <p>Hello <b>world!</b></p>
            <table>
                <title>table</title>
                <tbody>
                    <row>
                        <entry>aaa</entry>
                    </row>
                </tbody>
            </table>
        </body>
    </topic>

####Original input

---

##Shrinker

    [lang=xml]
    <topic>
        <title/>
        <body>
            <p><b>w</b></p>
        </body>
    </topic>

####Shrinked input

---

##Profits

1. Arbitrary input can detect edge cases - Correctness
2. Much simpler and consistent "Arrange phase" - Arrange
3. Easy to find minimal faulty input - 

---

##Drawbacks

1. "Assert phase" is more complex
2. Error reasoning can get tricky

---

##Questions?

***