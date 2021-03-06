﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    /// <summary>
    /// Given an array nums of n integers, are there elements a, b, c in nums such that a + b + c = 0?
    /// Find all unique triplets in the array which gives the sum of zero.Notice that the solution set must not contain duplicate triplets.
    /// Notice that the solution set must not contain duplicate triplets.
    /// </summary>
    /// <remarks>Medium</remarks>
    /// <seealso cref="https://leetcode.com/problems/3sum/"/>
    public sealed class ThreeSum
    {
        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[]
            {
                new[] {-1, 0, 1, 2, -1, -4},
                new[] {new[] {-1, -1, 2}, new[] {-1, 0, 1}}
            };
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Test(int[] input, int[][] output)
        {
            var result = Solve(input)
                .Select(x => x.ToArray())
                .ToArray();

            Assert.Equal(output.Length, result.Length);
            output = output.Select(x =>
            {
                Array.Sort(x);
                return x;
            }).ToArray();
            result = result.Select(x =>
            {
                Array.Sort(x);
                return x;
            }).ToArray();

            for (var i = 0; i < output.Length; i++)
            {
                Assert.Equal(output[i].Length, result[i].Length);

                for (var j = 0; j < output[i].Length; j++)
                {
                    Assert.Equal(output[i][j], result[i][j]);
                }
            }
        }

        public IList<IList<int>> Solve(int[] nums)
        {
            if (nums.Length < 3)
                return new List<IList<int>>(0);

            Array.Sort(nums);
            var result = new List<IList<int>>();

            for (var i = 0; i < nums.Length - 2; i++)
            {
                if (i > 0 && nums[i] == nums[i-1])
                    continue;

                if (nums[i] > 0)
                    break;

                var j = i + 1;
                var k = nums.Length - 1;

                while (j < k)
                {
                    var sum = nums[i] + nums[j] + nums[k];

                    if (sum > 0)
                    {
                        do
                        {
                            k--;
                        } while (j < k && (k == nums.Length - 1 || nums[k] == nums[k + 1]));
                    }
                    else
                    {
                        if (sum == 0)
                        {
                            result.Add(new List<int>(3) { nums[i], nums[j], nums[k] });

                            do 
                            {
                                k--;
                            } while (j < k && (k == nums.Length - 1 || nums[k] == nums[k + 1]));
                        }

                        do
                        {
                            j++;
                        } while (j < k && nums[j] == nums[j - 1]);
                    }
                }
            }

            return result;
        }
    }
}