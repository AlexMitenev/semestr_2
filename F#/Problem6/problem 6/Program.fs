let diff n =
  let mutable s = 0
  for i = 1 to n do
    for j = i + 1 to n do
      s <- s + i * j * 2
  s

printf "%A" <| diff 100