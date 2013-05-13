open System
open Core.Operators

let convertListToString list = List.fold (fun (acc : string) elem -> acc + string(elem)) "" list

type BigNumber(s : string) =

  let StringNumber = s

  member public this.ConvertToIntList =

    let CharArrNumber = StringNumber.ToCharArray()
    let IntArrNumber = Array.map (fun x -> Int32.Parse (string x)) CharArrNumber  
    Array.toList IntArrNumber 
  
  member public this.Value =
    this.ConvertToIntList

  member public this.Print =
   printfn "%s"  <| convertListToString(this.ConvertToIntList)

  member public this.Length =
    this.ConvertToIntList.Length

  member public this.deleteNulElem =
    let l = this.ConvertToIntList
    let rec delete l =    
      match l with
        | [] -> l
        | hd::tl when hd <> 0 -> l
        | hd:: tl when hd = 0 -> delete tl
    let goodNumber = new BigNumber(convertListToString <|delete l)
    goodNumber

type Ariphmetics =
  
  static member (+) (a:BigNumber, b:BigNumber) = Ariphmetics.Plus(a, b)
  static member (-) (a:BigNumber, b:BigNumber) = Ariphmetics.Minus(a, b)

  static member Balance(original : BigNumber, comparable : BigNumber)=
    
    let listN1 = original.Value
    let listN2 = comparable.Value


    let balance (originalList : int list) (comparableList : int list) =
      let differense = comparableList.Length - originalList.Length
      let listOfDiff = [ for i in 1 .. differense -> 0 ]
      let listAnswer = listOfDiff @ originalList
      listAnswer

    let stringAnswer = convertListToString <| balance listN1 listN2
    let answer = new BigNumber(stringAnswer)
    answer
    
  

  static member Plus(n1 : BigNumber, n2 : BigNumber) =

    let listN1 =  Ariphmetics.Balance(n1, n2).Value
    let listN2 =  Ariphmetics.Balance(n2, n1).Value

    let add (l1 : int list) (l2 : int list) =
      let listSum = List.foldBack2 (fun elem1 elem2 acc ->
                                    match acc with
                                    | [] -> ((elem1 + elem2) / 10) ::(((elem1 + elem2) % 10) :: acc)
                                    | hd::tl ->  ((elem1 + elem2 + hd) / 10)::((elem1 + elem2 + hd) % 10) :: tl)                                                             
                                    l1
                                    l2
                                    []
      listSum

    let stringNum = convertListToString <|add listN1 listN2
    let BigSum = new BigNumber(stringNum)
    BigSum.deleteNulElem

  static member Minus(n1 : BigNumber, n2 : BigNumber) =

    let listN1 =  Ariphmetics.Balance(n1, n2).Value
    let listN2 =  Ariphmetics.Balance(n2, n1).Value
    
    let sign x = if x < 0 then -1 else 0
    let modul x = if x < 0 then x + 10 else x

    let sub (l1 : int list) (l2 : int list) =
      let listSub = List.foldBack2 (fun elem1 elem2 acc ->
                                    match acc with
                                    | [] -> (sign(elem1 - elem2)) ::(modul(elem1 - elem2) :: acc)
                                    | hd::tl ->  (sign(elem1 - elem2 + hd))::(modul(elem1 - elem2 + hd)) :: tl)                                                             
                                    l1
                                    l2
                                    []
      listSub
    let stringNum = convertListToString <|sub listN1 listN2
    let BigSub = new BigNumber(stringNum)
    BigSub.deleteNulElem

  static member Multy(n1 : BigNumber, n2 : BigNumber) =
    
    let listN1 =  n1.Value
    let listN2 =  n2.Value

    let rec MulOneDigit x l1 = 
      match x with
      |0 -> new BigNum (ConvertToIntList l1)
      |n when n > 0 -> new BigNum (ConvertToIntList <|l1 + (MulOneDigit (n - 1) l1))
        


let BigN1 = new BigNumber("9999999999999999999999999999999999999999999999999999999999999999999999")
let BigN2 = new BigNumber("1")
let sum = Ariphmetics.Plus (BigN1, BigN2)
let sub = Ariphmetics.Minus (BigN1, BigN2)
sub.Print
sum.Print
