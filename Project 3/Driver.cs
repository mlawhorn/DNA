////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                                //
//	Project:	    Project 3                                                                                     //
//	File Name:		Driver.cs                                                                                   //
//	Description:	Take in a DNA file to predict eye color and chance of BRCA gene
//	Course:			CSCI 3230-001 - Algorithms                                                               //
//	Author:			Miranda Lawhorn, lawhornm@etsu.edu, Department of Computing, East Tennessee State University  //
//	Created:		Friday, March 29, 2019                                                                    //
//	Copyright:		Miranda Lawhorn, 2019                                                                         //
//                                                                                                                //
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    /// <summary>
    /// main class
    /// </summary>
    class Driver
    {
        /// <summary>
        /// main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int size = 701477;      //used to calculate list size   
            DNA dna = new DNA();    //instance of DNA class
            //set to read input from file given in console
            if (args.Length > 0)
            {
                string filepath = args[0];

                if (File.Exists(filepath))
                {
                    Console.SetIn(File.OpenText(filepath));
                }
            }
            string testBrown = "Not brown";   //used for initial eye color test
            List<DNA> strand = new List<DNA>(size);     //create list of DNA entries
            List<string> idOnly = new List<string>(size);   //create list of strings for rsid values
            int count = 0;
            String inputLine;
            string tab = "\t";
            //skip over first lines of data in file to get to start of DNA
            for(int i = 0; i < 18; i++)
            {
                Console.ReadLine();
            }
            //loop through to fill both lists
            while(count < strand.Capacity)
            {
                inputLine = Console.ReadLine();
                string[] vals = inputLine.Split(tab.ToCharArray()).ToArray();
                int chrom = int.Parse(vals[1]);
                int pos = int.Parse(vals[2]);
                strand.Add(new DNA(vals[0], chrom, pos, vals[3], vals[4]));
                idOnly.Add(vals[0]);
                count++;
            }
            string ifBrown = dna.InitialBlue(strand, idOnly);    //test for most likely blue
            if (!ifBrown.Equals(testBrown))
            {
                Console.WriteLine(ifBrown);      //if not equal, success, print 99% blue or 80% brown
            }
            else
            {
                string eyes = dna.Haplo1(strand, idOnly); //test to find eye color
                Console.WriteLine(eyes);                //output probability
            }
        }
    }
}
