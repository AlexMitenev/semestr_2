//Alex Mitenev 2013

let sum n =
  let mutable x1 = 1
  let mutable x2 = 2
  let mutable s = 2
  while x2 < n do
    let temp =  x1 + x2
    x1 <- x2
    x2 <- temp
    if x2 % 2 = 0 then s <- s + x2
  s
printf "%A" (sum 4000000)

    
    
  
