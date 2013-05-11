//Alex Mitenev 2013

type Tree =
  | Empty
  | Node of int * Tree * Tree

let rec addElement n tree =
  match tree with
    | Empty -> Node(n, Empty, Empty)
    | Node(value, left, right) when n < value -> Node(value, addElement n left, right)
    | Node(value, left, right) when n > value -> Node(value, left, addElement n right)
    |Node(value, left, right) when n = value -> Node(value, left, right)

let rec find tree n =
  match tree with
    | Empty -> false
    | Node(value, left, right) when n < value -> find left n
    | Node(value, left, right) when n > value -> find right n
    | Node(value, left, right) when n = value -> true

let rec findMin tree = 
    match tree with
      | Node(value, Empty, right) -> value
      | Node(value, left, right) -> findMin left


let rec deleteElement n tree =
  match tree with
    | Empty -> Empty
    | Node(value, left, right) when n < value -> Node(value, deleteElement n left, right)
    | Node(value, left, right) when n > value -> Node(value, left, deleteElement n right)

    | Node(value, Empty, Empty) when n = value -> Empty
    | Node(value, Empty, Node(valNext, leftNext, rightNext)) when n = value -> Node(valNext, leftNext, rightNext)
    | Node(value, Node(valNext, leftNext, rightNext), Empty) when n = value -> Node(valNext, leftNext, rightNext)
    | Node(value, left,  right) when n = value -> Node(findMin right, left, deleteElement (findMin right) right)

let rec printSpases num =
  match num with
    |0 -> printf "%s" ""
    |n when n < 0 -> printSpases 0
    |n when n > 0 -> printf "%c" ' '
                     printSpases (n - 1)

let rec print tree offset = 
  printSpases (offset - 2)
  if offset <> 0 then
    printf "%c" '|'
    printf "%c" '_'
    printf "%c" '_'
  match tree with
    | Empty ->printfn "%s" "Nill"
    | Node(value, left, right) -> printfn "%d" value
                                  //printSpases (offset)
                                  //printfn "%c" '|'
                                  print left (offset + 2)
                                  print right (offset + 2)

let MyTree =
  Empty
  |> addElement 5
  |> addElement 3
  |> addElement 0
  |> addElement 4
  |> addElement 6
  |> addElement 2
  |> addElement 9
  |> addElement 7
  |> deleteElement 5
print MyTree 0
