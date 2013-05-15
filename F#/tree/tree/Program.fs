//Alex Mitenev 2013

type Tree =
  | Empty
  | Node of int * Tree * Tree

let rec addElement n tree =
  match tree with
    | Empty -> Node(n, Empty, Empty)
    | Node(value, left, right) when n < value -> Node(value, addElement n left, right)
    | Node(value, left, right) when n > value -> Node(value, left, addElement n right)
    |Node(value, left, right) -> Node(value, left, right)

let rec find tree n =
  match tree with
    | Empty -> false
    | Node(value, left, right) when n < value -> find left n
    | Node(value, left, right) when n > value -> find right n
    | Node(value, left, right) -> true

let rec findMin tree =
    match tree with
      | Empty -> 0
      | Node(value, Empty, right) -> value
      | Node(value, left, right) -> findMin left


let rec deleteElement n tree =
  match tree with
    | Empty -> Empty
    | Node(value, left, right) when n < value -> Node(value, deleteElement n left, right)
    | Node(value, left, right) when n > value -> Node(value, left, deleteElement n right)

    | Node(value, Empty, Empty) -> Empty
    | Node(value, Empty, Node(valNext, leftNext, rightNext)) -> Node(valNext, leftNext, rightNext)
    | Node(value, Node(valNext, leftNext, rightNext), Empty) -> Node(valNext, leftNext, rightNext)
    | Node(value, left, right) -> Node(findMin right, left, deleteElement (findMin right) right)

let rec print tree offset =
  let toString (l : bool list) =
   List.foldBack(fun x acc ->
                   if x then acc + "|  "
                   else acc + "   ") 
                   l                                             
                   "" 
  match offset with
    | [] -> printf "%s" ""
    | [_] -> printf "%s" "|__"
    | hd::tl -> printf "%s" <| toString tl
                printf "%s" "|__"
                  
  match tree with
    | Empty ->printfn "%s" "Nill"
    | Node(value, left, right) -> 
                                  printfn "%d" value
                                  print left (true :: offset)
                                  print right (false :: offset) 

let MyTree =
  Empty
  |> addElement 5
  |> addElement 3
  |> addElement 0
  |> addElement 4
  |> addElement 6
  |> addElement 2
  |> addElement 9
print MyTree []
