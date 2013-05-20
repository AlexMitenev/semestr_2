open System
open Core.Operators

let convertListToString list =
  match list with
  |[] -> ""
  |hd::tl when hd < 0 -> "-" + List.fold (fun (acc : string) elem -> acc + string(elem * (-1))) "" list
  |hd::tl -> List.fold (fun (acc : string) elem -> acc + string(elem)) "" list

type BigNumber(s : string) =
  let Base = 10
  let StringNumber = s
  
  member public this.GreaterOrEqual(original : BigNumber) =

    let rec compare l1 l2 =
      match l1,l2 with
      |[],[] -> true
      |[], hd::tl when hd > 0 -> false
      |[], hd::tl -> true
      |hd::tl, [] when hd > 0 -> true
      |hd::tl, [] -> false
      |hd1::tl1, hd2::tl2 when hd1 < hd2 -> false
      |hd1::tl1, hd2::tl2 -> compare tl1 tl2

    compare this.Value original.Value
        
  member public this.ToIntList =
    let StringNumWithoutMinus = StringNumber.Trim([|'-'|])
    let CharArrNumber = StringNumWithoutMinus.ToCharArray()

    let sign c = if c = '-' then -1 else 1
    Array.toList <| Array.map (fun x -> Int32.Parse (string x) * sign(StringNumber.[0])) CharArrNumber
   
  member public this.IsEqual(num : BigNumber) =
    let rec compare l1 l2 =
      match l1, l2 with
      |[],[] -> true
      |[], hd::tl -> false
      |hd::tl, [] -> false
      |hd::tl, [] -> false
      |hd1::tl1, hd2::tl2 when hd1 <> hd2 -> false
      |hd1::tl1, hd2::tl2 -> compare tl1 tl2
    compare this.Value num.Value

  member public this.Negative =
    let conv l = List.foldBack (fun x acc -> (x*(-1)) :: acc) l []
    new BigNumber(convertListToString <| (conv this.Value))

  static member (~-) (n : BigNumber) =
    n.Negative

  member public this.Value =
    this.ToIntList

  member public this.Print =
   printfn "%s" <| convertListToString(this.Value)

  member public this.Length =
    this.ToIntList.Length

  member public this.DeleteLeadingZero =
    let l = this.Value
    let rec delete l =
      match l with
        | [] -> []
        | [0] -> [0]
        | hd::tl when hd <> 0 -> l
        | hd:: tl -> delete tl
    new BigNumber(convertListToString <|delete l)

  static member Balance(this : int list, compare : int list) =
    let balance (l1 : int list) (l2 : int list) =
      let differense = l2.Length - l1.Length
      [ for i in 1 .. differense -> 0 ] @l1
    balance this compare

  member public this.Plus(summand : BigNumber) =
    let Zero = new BigNumber("0")
    let thisList = BigNumber.Balance (this.Value,summand.Value)
    let summandList = BigNumber.Balance (summand.Value, this.Value)

    let sign x =
      match x with
      |n when n < 0 -> -1
      |n when n >= Base -> 1
      |n -> 0

    let modul x =
      match x with
      |n when n < 0 -> n + Base
      |n when n >= Base -> n - Base
      |n -> n

    let add (l1 : int list) (l2 : int list) =
      let listSum = List.foldBack2 (fun elem1 elem2 acc ->
                                    match acc with
                                    | [] -> (sign <| elem1 + elem2) ::((modul <| elem1 + elem2) :: acc)
                                    | hd::tl -> (sign <| elem1 + elem2 + hd)::(modul <| elem1 + elem2 + hd) :: tl)
                                    l1
                                    l2
                                    []
      listSum
    let listRes = add thisList summandList   

    let normalizationNegative (list : int list) =
      let num = list.Tail
      let putZero n (l : int list) = l @ [ for i in 1 .. n -> 0 ]
      let listNegative l = List.map (fun x -> x*(-1)) l
      add (putZero num.Length [1]) (0::(listNegative num))

    match listRes with
    |[] -> Zero
    |hd::tl when hd = 0 -> let NumWithZero = new BigNumber(convertListToString <|listRes)
                           NumWithZero.DeleteLeadingZero
    |hd::tl when hd = -1 -> let NumWithZero = BigNumber(convertListToString <|normalizationNegative listRes)
                            -NumWithZero.DeleteLeadingZero
    |hd::tl -> new BigNumber(convertListToString <|listRes)

  static member (+) (n1 : BigNumber, n2 : BigNumber) =
    n1.Plus(n2)

  member public this.Minus(subtrahend : BigNumber) =
    -(subtrahend + (-this))

  static member (-) (n1 : BigNumber, n2 : BigNumber) =
    n1.Minus(n2)

  member public this.Multyply(factor : BigNumber) =
    let Zero = new BigNumber("0")

    let rec mulOneDigit (acc : BigNumber) n =
      match n with
      |0 -> acc
      |n when n > 0 -> mulOneDigit (acc + this) (n-1)
      |n -> mulOneDigit (acc - this) (n + 1)
    
    let putZero n (big : BigNumber) =
      let value = big.Value @ [ for i in 1 .. n -> 0 ]
      new BigNumber(convertListToString value)

    let rec mul l accNum (acc : BigNumber) =
      match l with
      | [] -> acc
      | hd::tl -> acc + (mulOneDigit Zero hd |> putZero accNum) + (mul tl (accNum - 1) Zero)
    mul factor.Value (factor.Length - 1) Zero

  static member (*) (n1 : BigNumber, n2 : BigNumber) =
    n1.Multyply(n2)

let test testName (state1 : BigNumber) (state2 : BigNumber)=
  if not <| state1.IsEqual(state2) then printf "%s" "false. Problem in test:"
                                        printfn "%s" testName

let Number = new BigNumber("12345")
let Number2 = new BigNumber("100")
let NegativeNumber = new BigNumber("-12347")
let LongNumber = new BigNumber("12345123123123234234234234234234234234234")
let Zero = new BigNumber("0")

test "Add" (Number + Number)  (new BigNumber("24690"))
test "AddNegativeWithNormal" (Number + NegativeNumber)  (new BigNumber("-2"))
test "AddNegativeWithNegative" (-Number + NegativeNumber)  (new BigNumber("-24692"))
test "AddZero" (Number + Zero)  (new BigNumber("12345"))
test "Sub" (Number - Number2)  (new BigNumber("12245"))
test "SubNegative" (Number2 - NegativeNumber)  (new BigNumber("12447"))
test "SubWithNegativeResult" (Number2 - Number)  (new BigNumber("-12245"))
test "Negative" (-Number2)  (new BigNumber("-100"))
test "Multyple" (Number2 * Number)  (new BigNumber("1234500"))
test "Multyple2Negative" (-Number2 * (-Number))  (new BigNumber("1234500"))
test "MultypleWithNegative" (-Number2 * Number)  (new BigNumber("-1234500"))
test "MultypleWithZero" (-Number2 * Zero) (new BigNumber("0"))
test "MultypleLongNumber" (Number2 * LongNumber) (new BigNumber("1234512312312323423423423423423423423423400"))
test "wrongTest" (Number2 + Number2) (new BigNumber("32"))
