using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 随机数工具类
    /// </summary>
    public static class RandomUtil
    {

        /// <summary>
        /// 计算概率
        /// </summary>
        /// <param name="probabilityValue"></param>
        /// <returns></returns>
        public static int calcProbability(float[] probabilityValue)
        {
            float total = 0; //首先计算出概率的总值，用来计算随机范围 
            for (int i = 0; i < probabilityValue.Length; i++)
            {
                total += probabilityValue[i];
            }
            float Nob = UnityEngine.Random.Range(0, total);
            for (int i = 0; i < probabilityValue.Length; i++)
            {
                if (Nob < probabilityValue[i])
                {
                    return i;
                }
                else
                {
                    Nob -= probabilityValue[i];
                }
            }
            return probabilityValue.Length - 1;
        }

        public static int getRandomInteger(int min, int max)
        {
            return new System.Random(getRandomSeed()).Next(min, max);
        }

        private static int getRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static int[] getRandomArray(int count, int minNum, int maxNum)
        {
            int j;
            int[] b = new int[count];
            System.Random r = new System.Random();
            for (j = 0; j < count; j++)
            {
                int i = r.Next(minNum, maxNum + 1);
                int num = 0;
                for (int k = 0; k < j; k++)
                {
                    if (b[k] == i)
                    {
                        num = num + 1;
                    }
                }
                if (num == 0)
                {
                    b[j] = i;
                }
                else
                {
                    j = j - 1;
                }
            }
            return b;
        }

        public static float getRandomFloat0_1()
        {
            return UnityEngine.Random.value;
        }

        public static string getRandomChinese(int strlength)
        {
            // 获取GB2312编码页（表） 
            Encoding gb = Encoding.GetEncoding("gb2312");

            object[] bytes = createRegionCode(strlength);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < strlength; i++)
            {
                string temp = gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
                sb.Append(temp);
            }

            return sb.ToString();
        }


        /// <summary>
        /// 此函数在汉字编码范围内随机创建含两个元素的十六进制字节数组，每个字节数组代表一个汉字，并将 
        /// 四个字节数组存储在object数组中。 
        //  参数：strlength，代表需要产生的汉字个数 
        /// </summary>
        /// <param name="strlength"></param>
        /// <returns></returns>
        private static object[] createRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素 
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            System.Random rnd = new System.Random();

            //定义一个object数组用来 
            object[] bytes = new object[strlength];

            /**
             每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bytes数组中 
             每个汉字有四个区位码组成 
             区位码第1位和区位码第2位作为字节数组第一个元素 
             区位码第3位和区位码第4位作为字节数组第二个元素 
            **/
            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位 
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //区位码第2位 
                rnd = new System.Random(r1 * unchecked((int)DateTime.Now.Ticks) + i); // 更换随机数发生器的 种子避免产生重复值 
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位 
                rnd = new System.Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位 
                rnd = new System.Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();

                // 定义两个字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                // 将两个字节变量存储在字节数组中 
                byte[] str_r = new byte[] { byte1, byte2 };

                // 将产生的一个汉字的字节数组放入object数组中 
                bytes.SetValue(str_r, i);
            }

            return bytes;
        }

        private static string firstName = @"赵,钱,孙,李,周,吴,郑,王,冯,陈,褚,卫,蒋,
            沈,韩,杨,朱,秦,尤,许,何,吕,施,张,孔,曹,严,华,金,魏,陶,姜, 戚,谢,邹,喻,
            柏,水,窦,章,云,苏,潘,葛,奚,范,彭,郎,鲁,韦,昌,马,苗,凤,花,方,俞,任,袁,柳,
            丰,鲍,史,唐, 费,廉,岑,薛,雷,贺,倪,汤,滕,殷,罗,毕,郝,邬,安,常,乐,于,时,
            傅,皮,卞,齐,康,伍,余,元,卜,顾,孟,平,黄, 和,穆,萧,尹,姚,邵,湛,汪,祁,毛,
            禹,狄,米,贝,明,臧,计,伏,成,戴,谈,宋,茅,庞,熊,纪,舒,屈,项,祝,董,梁, 杜,
            阮,蓝,闵,席,季,麻,强,贾,路,娄,危,江,童,颜,郭,梅,盛,林,刁,钟,徐,丘,骆,高,
            夏,蔡,田,樊,胡,凌,霍, 虞,万,支,柯,昝,管,卢,莫,经,房,裘,缪,干,解,应,宗,丁,
            宣,贲,邓,郁,单,杭,洪,包,诸,左,石,崔,吉,钮,龚, 程,嵇,邢,滑,裴,陆,荣,翁,荀,
            羊,於,惠,甄,麴,家,封,芮,羿,储,靳,汲,邴,糜,松,井,段,富,巫,乌,焦,巴,弓, 牧,
            隗,山,谷,车,侯,宓,蓬,全,郗,班,仰,秋,仲,伊,宫,宁,仇,栾,暴,甘,钭,厉,戌,祖,
            武,符,刘,景,詹,束,龙, 叶,幸,司,韶,郜,黎,蓟,薄,印,宿,白,怀,蒲,邰,从,鄂,索,
            咸,籍,赖,卓,蔺,屠,蒙,池,乔,阴,郁,胥,能,苍,双, 闻,莘,党,翟,谭,贡,劳,逢,姬,
            申,扶,堵,冉,宰,郦,雍,郤,璩,桑,桂,濮,牛,寿,通,边,扈,燕,冀,郏,浦,尚,农, 温,
            别,庄,晏,柴,瞿,阎,充,慕,连,茹,习,宦,艾,鱼,容,向,古,易,慎,戈,廖,庾,终,暨,
            居,衡,步,都,耿,满,弘, 匡,国,文,寇,广,禄,阙,东,欧,殳,沃,利,蔚,越,菱,隆,师,
            巩,厍,聂,晃,勾,敖,融,冷,訾,辛,阚,那,简,饶,空, 曾,毋,沙,乜,养,鞠,须,丰,巢,
            关,蒯,相,查,后,荆,红,游,竺,权,逯,盖,益,桓,公, 万俟,司马,上官,欧阳,夏侯,
            诸葛,闻人,东方,赫连,皇甫,尉迟,公羊,澹台,公冶,宗政,濮阳,淳于,单于,太叔,
            申屠,公孙,仲孙,轩辕,令狐,钟离,宇文,长孙,慕容,司徒,司空";

        private static string lastName = @"努,迪,立,林,维,吐,丽,新,涛,米,亚,克,湘,明,
            白,玉,代,孜,霖,霞,加,永,卿,约,小,刚,光,峰,春,基,木,国,娜,晓,兰,阿,伟,英,元,
            音,拉,亮,玲,木,兴,成,尔,远,东,华,旭,迪,吉,高,翠,莉,云,华,军,荣,柱,科,生,昊,
            耀,汤,胜,坚,仁,学,荣,延,成,庆,音,初,杰,宪,雄,久,培,祥,胜,梅,顺,涛,西,库,康,
            温,校,信,志,图,艾,赛,潘,多,振,伟,继,福,柯,雷,田,也,勇,乾,其,买,姚,杜,关,陈,
            静,宁,春,马,德,水,梦,晶,精,瑶,朗,语,日,月,星,河,飘,渺,星,空,如,萍,棕,影,南,北";

        static System.Random rnd = new System.Random((int)DateTime.Now.ToFileTimeUtc());

        public static string getRandomName()
        {
            int namelength = 0;
            namelength = rnd.Next(2, 4);
            firstName = firstName.Replace("\n", "");
            firstName = firstName.Replace("\r", "");
            firstName = firstName.Replace(" ", "");
            lastName = lastName.Replace("\r", "");
            lastName = lastName.Replace("\n", "");
            lastName = lastName.Replace(" ", "");
            string name = "";
            string[] FirstName = firstName.Split(',');
            string[] LastName = lastName.Split(',');
            if (namelength == 2)
            {
                name = FirstName[rnd.Next(0, FirstName.Length)] + LastName[rnd.Next(0, LastName.Length)];
            }
            else if (namelength == 3)
            {
                name = FirstName[rnd.Next(0, FirstName.Length)] + LastName[rnd.Next(0, LastName.Length)] + LastName[rnd.Next(0, LastName.Length)];
            }

            return name;
        }

        public static string GetRandomNumber(int startnumber, int endnumber)
        {
            string strNumber = rnd.Next(startnumber, endnumber).ToString();
            return strNumber;
        }


        public static long getRandomInteger(int length)
        {
            string buffer = "0123456789";// 随机字符中也可以为汉字（任何）  
            StringBuilder sb = new StringBuilder();
            System.Random r = new System.Random();
            int range = buffer.Length;
            for (int i = 0; i < length; i++)
            {
                sb.Append(buffer.Substring(r.Next(range), 1));
            }
            return Convert.ToInt64(sb.ToString());
        }
    }
}
