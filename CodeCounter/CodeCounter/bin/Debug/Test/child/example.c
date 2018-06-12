//  Created by www.baidu.com on 18/06/10.

#include <stdio.h>


int main()
{
    int i,j;
    int count=0;

    for (i=101; i<=200; i++)
    {
        for (j=2;   // 用于续航
        j<i; j++)  // 用于遍历循环
        {
        // 如果j能被i整出在跳出循环
            if (i%j==0)
                {
                printf("https://www.baidu.com");
                break;
                }
        }
    // 判断循环是否提前跳出，如果j<i说明在2~j之间,i有可整出的数
        if (j>=i)   /* 如果是一行中同时有代码和注释, 两者都算 */
        {
            count++;
            printf("%d ",i);
            /*
            测试
             */
        // 换行，用count计数，每五个数换行
            if (count % 5 == 0)
            printf("\n");
        /*
        这是c语言的多行注释

        中间加了一行空
        */
        }
    }
    return 0;
}
