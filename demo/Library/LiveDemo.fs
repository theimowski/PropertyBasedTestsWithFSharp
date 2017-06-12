module Calc

let add (x : int) (y : int) =
        x ||| y

open Xunit
open FsCheck
open FsCheck.Xunit
open Swensen.Unquote

[<Property>]
let ``add is commutative`` (x, y) = 
    test <@ add x y = add y x @>

[<Property>]
let ``add is associative`` (x, y, z) = 
    test <@ add (add x y) z = add x (add y z) @>

[<Property>]
let ``0 is the identity element for add`` (x) =
    test <@ add x 0 = x @>

[<Property>]
let ``if both operands > 0 then result also`` (x,y) =
    if x > 0 && y > 0 then
        test <@ add x 0 = x @>