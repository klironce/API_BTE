using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace btcetest
{
    class Parse
    {

        public struct DataStek
        {

            public DataStek(string a)
            {
               skobcount = 0;
               PMathcount = 0;
               scob = new int[1,2];
               pmath = new int[1];
               foundSkobs(a);
               createPMath(a);
            }

            private bool cheackAlfabet(char ch,bool first)
            {
                if (first)
                {
                    if ((ch == '*' || ch == ':'))
                        return true;
                    return false;
                }
                else
                {
                    if ((ch == '+' || ch == '-'))
                        return true;
                    return false;
                }
            }

            public int skobcount;
            public int[,] scob;
            private void getPMathInSkob(int s, int f, string a)
            {
                for (int i = s + 1; i < f; i++)
                    if (cheackAlfabet(a[i], true))
                        addPMath(i);


                for (int i = s + 1; i < f; i++)
                    if (cheackAlfabet(a[i], false))
                        addPMath(i);
            }
            private void addSkob(int p,string a)
            {
                int elseSkobCount = 0;
                int pEnd = 0;
                for (int i = p+1; i < a.Length; i++)
                {
                    if (a[i] == '(')
                        elseSkobCount++;
                    else
                        if (a[i] == ')')
                            if (elseSkobCount != 0)
                                elseSkobCount--;
                            else
                            {
                                pEnd = i;
                                break;
                            }

                }

                if(skobcount == 0)
                {
                    scob[0, 0] = p;
                    scob[0, 1] = pEnd;
                }
                else
                {
                    int[,] backUp = new int[skobcount, 2];
                    for (int i = 0; i < skobcount; i++)
                    {
                        backUp[i, 0] = scob[i, 0];
                        backUp[i, 1] = scob[i, 1];
                    }
                    scob = new int[skobcount + 1, 2];
                    for (int i = 0; i < skobcount; i++)
                    {
                        scob[i, 0] = backUp[i, 0];
                        scob[i, 1] = backUp[i, 1]; 
                    }
                    scob[skobcount , 0] = p;
                    scob[skobcount , 1] = pEnd;
                }
                skobcount++;
            }
            private void foundSkobs(string a)
            {
                for (int i = 0; i < a.Length; i++)
                    if (a[i] == '(')
                        addSkob(i, a);
            }

            public int PMathcount;
            public int[] pmath;
            private bool cheackPMathUsed(int p)
            {
                for (int i = 0; i < PMathcount; i++)
                    if (pmath[i] == p)
                        return false;

                return true;
            }
            private void addPMath(int p)
            {
                if (cheackPMathUsed(p)) { 
                    if (PMathcount == 0)
                    {
                        pmath[0] = p;
                    }
                    else
                    {
                        int[] backUp = new int[PMathcount];
                        for (int i = 0; i < PMathcount; i++)
                            backUp[i] = pmath[i];
                        pmath = new int[PMathcount + 1];
                        for (int i = 0; i < PMathcount; i++)
                            pmath[i] = backUp[i];
                        pmath[PMathcount] = p;
                    }
                    PMathcount++;
                }
            }
            private void addOtherPMath(string a)
            {
                for (int i = 0; i < a.Length; i++)
                    if (cheackAlfabet(a[i], true))
                        addPMath(i);

                for (int i = 0; i < a.Length; i++)
                    if (cheackAlfabet(a[i], false))
                        addPMath(i);
            }
            private void createPMath(string a)
            {
                if (skobcount != 0)
                {
                    for (int i = skobcount - 1; i != -1; i--)
                    {
                        getPMathInSkob(scob[i, 0], scob[i, 1], a);
                    }

                    addOtherPMath(a);
                }
            }

        }

        public static string getColumnName(string str, bool end)
        {
            int p = 0;
            string nstr = "";
            for (int i = 0; i < str.Length; i++)
                if (str[i] == '_') p = i;

                if (!end)
                {
                    for (int i = 0; i < p; i++)
                        nstr += str[i];
                }
                else
                {
                    for (int i = p + 1; i < str.Length; i++)
                        nstr += str[i];
                }

                return nstr;
        }



    }
}
