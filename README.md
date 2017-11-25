# CompetitiveProgramming

競技プログラミング用のライブラリです。すべて C# 7.0 で実装されています。

## Contents

### 一般的なアルゴリズム [[Algorithms](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Algorithms)]

- 二分探索 [[BinarySearch.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Algorithms/BinarySearch.cs)]
- 座標圧縮 [[CoordinateCompressor.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Algorithms/CoordinateCompressor.cs)]
- 擬似乱数生成 (Xorshift32) [[Xorshift32.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Algorithms/Xorshift32.cs)]

### データ構造 [[Collections](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Collections)]

- 素集合データ構造 [[UnionFind.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/UnionFind.cs)]

### グラフ [[Graphs](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Graphs)]

- 隣接行列 [[AdjacencyMatrix.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Graphs/AdjacencyMatrix.cs)]
- 全点対最短経路探索 (Warshall-Floyd) [[WarshallFloyd.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Graphs/WarshallFloyd.cs)]

### 数学 [[Math](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Math)]

- 多倍長有理数 [[BigRational.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Math/BigRational.cs)]
- (色々 [[MathExtensions.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Math/MathExtensions.cs)])
  - GCD (Gcd)
  - LCM (Lcm)
  - 与えられた非負整数を超えない最大の 2 べき (HighestOneBit)
  - 累乗 (Pow)
  - 階乗 (Fact)

### 区間クエリ [[RangeQuery](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/RangeQuery)]

- Fenwick Tree (Binary Indexed Tree) [[FenwickTree.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/RangeQuery/FenwickTree.cs)]
- いもす法 [[Imos.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/RangeQuery/Imos.cs)]
- Segment Tree [[SegmentTree.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/RangeQuery/SegmentTree.cs)]

### 拡張 [[Extensions](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Extensions)]

- (色々 [[Extensions.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Extensions/Extensions.cs)])
- 群 [[Group.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Extensions/Group.cs)]
- モノイド [[Monoid.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Extensions/Monoid.cs)]

### その他 [[Others](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Others)]

- パーサコンビネータ [[Parser.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Others/Parser.cs)]
- テキストスキャナ (?) [[Scanner.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Others/Scanner.cs)]
  - 競技プログラミングでよくある入力を高速パースするやつ

## License

特に記述がない部分はすべて [WTFPL](https://github.com/kuretchi/CompetitiveProgramming/blob/master/LICENSE.txt) です。
