using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAutoFac.Leetcode
{
    public class _042_Trapping_Rain_Water
    {
        public static void Executer()
        {
            int[] a = new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            int res = WaterTrapped(a);
            int res2 = trap(a);
        }

        /// <summary>
        /// Here is my idea: instead of calculating area by height*width, we can think it in a cumulative way. In other words, sum water amount of each bin(width=1).
        ///Search from left to right and maintain a max height of left and right separately, which is like a one-side wall of partial container.Fix the higher one and flow water from the lower part.
        ///For example, if current height of left is lower, we fill water in the left bin.Until left meets right, we filled the whole container.
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int trap(int[] height)
        {
            int left = 0;
            int right = 0;
            int low = 0;
            int high = height.Length - 1;
            int ans = 0;
            while (low < high)
            {
                left = Math.Max(left, height[low]);
                right = Math.Max(right, height[high]);
                if (left < right)
                {
                    ans += left - height[low];
                    low++;
                }
                else
                {
                    ans += right - height[high];
                    high--;
                }
            }
            return ans;
        }
        public static int WaterTrapped(int[] height)
        {
            if (height == null || height.Length == 0)
                return 0;

            int n = height.Length;
            int res = 0;

            int[] leftMax = new int[n];
            int[] rightMax = new int[n];

            leftMax[0] = height[0];
            for (int i = 1; i < n; i++)
                leftMax[i] = Math.Max(leftMax[i - 1], height[i]);

            rightMax[n-1] = height[n-1];
            for (int i = n - 2; i >= 0; i--)
                rightMax[i] = Math.Max(rightMax[i + 1], height[i]);

            for (int i = 0; i < n; i++)
                res += Math.Min(leftMax[i], rightMax[i]) - height[i];
            return res;
        }
    }
}
