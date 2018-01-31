# CompetitiveProgramming

競技プログラミング用のライブラリです。C# 6.0 で実装されています。

## Contents

### 一般的なアルゴリズム [[Algorithms](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Algorithms)]

- 二分探索 [[BinarySearch.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Algorithms/BinarySearch.cs)]
- 座標圧縮 [[CoordinateCompressor.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Algorithms/CoordinateCompressor.cs)]
- 擬似乱数生成 (Xorshift32) [[Xorshift32.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Algorithms/Xorshift32.cs)]

### データ構造 [[Collections](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Collections)]

- 二分ヒープ [[BinaryHeap.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/BinaryHeap.cs)]
- 優先度付きキュー [[IPriorityQueue.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/IPriorityQueue.cs)]
- Leftist ヒープ [[LeftistHeap.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/LeftistHeap.cs)]
- 素集合データ構造 [[UnionFind.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/UnionFind.cs)]

#### 区間クエリ [[RangeQuery](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Collections/RangeQuery)]

- Fenwick Tree (Binary Indexed Tree) [[FenwickTree.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/RangeQuery/FenwickTree.cs)]
- いもす法 [[Imos.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/RangeQuery/Imos.cs)]
- 2次元いもす法 [[Imos2D.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/RangeQuery/Imos2D.cs)]
- 遅延伝播 Segment Tree [[LazySegmentTree.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/RangeQuery/LazySegmentTree.cs)]
- Segment Tree [[SegmentTree.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Collections/RangeQuery/SegmentTree.cs)]

### グラフ [[Graphs](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Graphs)]

- 隣接行列 [[AdjacencyMatrix.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Graphs/AdjacencyMatrix.cs)]
- 全点対最短経路探索 (Warshall-Floyd) [[WarshallFloyd.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Graphs/WarshallFloyd.cs)]

### 数学 [[Math](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Math)]

- 多倍長有理数 [[BigRational.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Math/BigRational.cs)]
- 素数列挙 (エラトステネスの篩) [[Eratosthenes.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Math/Eratosthenes.cs)]
- (色々 [[MathExtensions.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Math/MathExtensions.cs)])
  - 切り上げ除算 (DivCeil)
  - GCD (Gcd)
  - LCM (Lcm)
  - 与えられた非負整数を超えない最大の 2 べき (HighestOneBit)
  - 累乗 (Pow)
  - 階乗 (Fact)
- 定数で常に剰余を取った数 [[ModInt32.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Math/ModInt32.cs)]

### 拡張 [[Extensions](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Extensions)]

- 二項演算 [[BinaryOperator.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Extensions/BinaryOperator.cs)]
- よく使う定数 [[Constants.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Extensions/Constants.cs)]
- (色々 [[Extensions.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Extensions/Extensions.cs)])
- 群 [[Group.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Extensions/Group.cs)]
- モノイド [[Monoid.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Extensions/Monoid.cs)]

### その他 [[Others](https://github.com/kuretchi/CompetitiveProgramming/tree/master/CompetitiveProgramming/Others)]

- パーサコンビネータ [[Parser.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Others/Parser.cs)]
- スキャナ (高速版) [[StreamScanner.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Others/StreamScanner.cs)]
- スキャナ [[TextScanner.cs](https://github.com/kuretchi/CompetitiveProgramming/blob/master/CompetitiveProgramming/Others/TextScanner.cs)]
