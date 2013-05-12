//Alex Mitenev 2013

let addToEnd l n = 
  let rec add l =
    match l with
      | [] -> n :: l
      | hd::tl -> hd :: (add tl)
  add l   

let rec append l1 l2 = 
  match l1 with
    | [ n ] -> n :: l2
    | hd::tl -> hd :: (append tl l2)
    | [] -> l2
  
let reverse l =
  let l2 = []
  let rec rev l l2 = 
    match l with
      |[] -> l2
      |hd::tl ->  rev tl (hd::l2)
  rev l l2

let isEven n =
  n % 2 = 0

let rec find l f =
    match l with
    | hd::tl when f hd -> Some hd
    | hd::tl when not <| f hd -> find tl f
    | [] -> None

let map l f = List.foldBack (fun x acc -> (f x) :: acc) l []

let test testName state1 state2=
  if state1 <> state2 then printf "%s" "false. Problem in test:"
                           printfn "%s" testName

test "testAddToEnd" (addToEnd [1;2;3;4;5] 6)  [1;2;3;4;5;6]
test "testAddToEndWithEmptyList" (addToEnd [] 6) [6]
test "testAppend" (append [1;2;3] [4;5;6])  [1;2;3;4;5;6]
test "testAppendWithEmptyList" (append [] [1;2;3]) [1;2;3]
test "testReverse" (reverse [1;2;3]) [3;2;1]
test "testReverseWithEmptyList" (reverse []) []
test "testFindWithTrue" (find [1;2;3;4] isEven) (Some 2)
test "testFindWithFalse" (find [1;5;3;7] isEven) None
test "testFindWithEmptyList" (find [] isEven) None
test "testMap" (map [1;2;3] isEven) [false;true;false]
test "testMapWithEmptyList" (map [] isEven) []