//Alex Mitenev

let rec gcd (a : int64) (b: int64) = 
  if a % b = 0L then b 
  else gcd b <| a % b
   
let rec lcm (n : int64) =
    if n = 1L then 1L
    else lcm (n - 1L) * n / gcd (lcm (n - 1L)) n

printf "%A" <| lcm 20L
