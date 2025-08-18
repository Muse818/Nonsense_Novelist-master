using System;
using System.Collections.Generic;
using System.Linq;

public class WeightedLottery<T>
{
    private readonly List<ItemPool> _pools;
    private readonly Random _random;

    // 奖池定义
    private class ItemPool
    {
        public double Probability { get; }   // 该池的抽中概率
        public List<T> Items { get; }        // 池内物品列表
        public double CumulativeProbability { get; set; }  // 累积概率

        public ItemPool(double probability, IEnumerable<T> items)
        {
            Probability = probability;
            Items = items.ToList();
        }
    }

    public WeightedLottery()
    {
        _pools = new List<ItemPool>();
        _random = new Random();
    }

    /// <summary>
    /// 添加奖池
    /// </summary>
    /// <param name="probability">该池被抽中的概率（0-1之间）</param>
    /// <param name="items">池内物品集合</param>
    public void AddPool(double probability, IEnumerable<T> items)
    {
        if (probability <= 0)
            throw new ArgumentException("概率必须大于0", nameof(probability));

        var pool = new ItemPool(probability, items);
        _pools.Add(pool);
    }

    /// <summary>
    /// 执行抽取操作
    /// </summary>
    /// <returns>抽中的物品</returns>
    public T Draw()
    {
        if (!_pools.Any())
            throw new InvalidOperationException("没有可用的奖池");

        ValidateProbabilities();
        CalculateCumulativeProbabilities();

        // 1. 选择奖池
        double poolRng = _random.NextDouble();
        ItemPool selectedPool = _pools.First(p => poolRng <= p.CumulativeProbability);

        // 2. 从选中的奖池中随机抽取物品
        int itemIndex = _random.Next(selectedPool.Items.Count);
        return selectedPool.Items[itemIndex];
    }

    // 计算累积概率
    private void CalculateCumulativeProbabilities()
    {
        double total = _pools.Sum(p => p.Probability);
        double cumulative = 0;

        foreach (var pool in _pools)
        {
            cumulative += pool.Probability / total;
            pool.CumulativeProbability = cumulative;
        }
    }

    // 验证概率有效性
    private void ValidateProbabilities()
    {
        if (_pools.Sum(p => p.Probability) <= 0)
            throw new InvalidOperationException("总概率必须大于0");
    }
}
