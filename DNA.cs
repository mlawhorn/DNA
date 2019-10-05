////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                                //
//	Project:	    Project 3                                                                                     //
//	File Name:		DNA.cs                                                                                   //
//	Description:	Create a value to hold all components of an SNP value and perform tests to predict
//                  eye color and chance of carrying the BRCA gene
//	Course:			CSCI 3230-001 - Algorithms                                                               //
//	Author:			Miranda Lawhorn, lawhornm@etsu.edu, Department of Computing, East Tennessee State University  //
//	Created:		Friday, March 29, 2019                                                                    //
//	Copyright:		Miranda Lawhorn, 2019                                                                         //
//                                                                                                                //
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    /// <summary>
    /// class used for DNA testing
    /// </summary>
    class DNA
    {
        public string rsid;     //SNP identifier
        public int chromosome;  //chromosome number
        public int position;    //basepair position of SNP
        public string allele1;  //first allele for genotype
        public string allele2;  //second allele for genotype

        /// <summary>
        /// no arg constructor
        /// </summary>
        public DNA()
        {

        }

        /// <summary>
        /// parameterized constructor
        /// </summary>
        /// <param name="rsid">identifier</param>
        /// <param name="chromosome">chromosome position</param>
        /// <param name="position">basepair position</param>
        /// <param name="allele1">allele 1 genotype</param>
        /// <param name="allele2">allele 2 genotype</param>
        public DNA(string rsid, int chromosome, int position, string allele1, string allele2)
        {
            this.rsid = rsid;
            this.chromosome = chromosome;
            this.position = position;
            this.allele1 = allele1;
            this.allele2 = allele2;
        }

        /// <summary>
        /// test SNP identifier most commonly associated with 99% blue eyes and 80% brown eyes
        /// </summary>
        /// <param name="strand">list of DNA</param>
        /// <param name="id">list of rsid values</param>
        /// <returns>probability of blue or brown if matching allele pair, otherwise "Not blue"</returns>
        public string InitialBlue(List<DNA> strand, List<string> id)
        {
            string find1 = "rs12913832";    //rsid to test
            string al1;                     //holder for allele1
            string al2;                     //holder for allele2
     
            int index = id.FindIndex(x => x.StartsWith(find1)); //try to find index of rsid in DNA
            if(index > -1)
            {
                //if found, use index to fill in allele1 and allele2
                al1 = strand[index].allele1;
                al2 = strand[index].allele2;
                //if the first allele isn't a G, 80% brown
                if(!al1.Equals("G"))
                {
                    index = id.FindIndex(x => x.StartsWith("rs1667394")); //try to find index of rsid in DNA
                    if(index > -1)
                    {
                        al1 = strand[index].allele1;
                        al2 = strand[index].allele2;
                        if(al1.Equals("T") || al2.Equals("T"))
                        {
                            return "Not brown";
                        }
                        else
                        {
                            return "Brown: 80%, Not-Brown: 20%";
                        }
                    }
                   
                }
                //else return a false and move on to do more testing
                else
                {
                    return "Not brown";
                }
               
            }
            return "Not brown";
        }

        /// <summary>
        /// Perform further testing on DNA file to predict eye color
        /// </summary>
        /// <param name="strand">list of DNA</param>
        /// <param name="id">list of rsid values</param>
        /// <returns></returns>
        public string Haplo1(List<DNA> strand, List<string> id)
        {
            int index;      //index of rsid
            string al1;                     //holder for allele1
            string al2;                     //holder for allele2
            index = id.FindIndex(x => x.StartsWith("rs1129038"));   //try to find rsid
            if(index > -1)
            {
                //if found, use index to fill in allele1 and allele2
                al1 = strand[index].allele1;
                al2 = strand[index].allele2;
				if ((al1.Equals(al2) && al1.Equals("C")) || (!al1.Equals(al2))) 
				{
                    //if CC or CT, move on to next rsid to test
					index = id.FindIndex(x => x.StartsWith("rs1800407"));
                    if (index > -1)
                    {
                        al1 = strand[index].allele1;
                        al2 = strand[index].allele2;
                        //if homozygous pair, test next rsid
                        if(al1.Equals(al2))
                        {
                            index = id.FindIndex(x => x.StartsWith("rs12913832"));
                            if (index > -1)
                            {
                                al1 = strand[index].allele1;
                                al2 = strand[index].allele2;
                                //if homozygous pair, 100% chance of brown
                                if (al1.Equals(al2))
                                {
                                    return "Brown";
                                }
                                //if heterozygous, test next rsid
                                else
                                {
                                    index = id.FindIndex(x => x.StartsWith("rs1393350"));
                                    if (index > -1)
                                    {
                                        al1 = strand[index].allele1;
                                        al2 = strand[index].allele2;
                                        if (al1.Equals(al2))
                                        {
                                            //if CC or GG, 75% chance of brown
                                            if (al1.Equals("G") || al1.Equals("C"))
                                            {
                                                return "Brown: 75%, Non-Brown: 25%";
                                            }
                                          
                                        }
                                        
                                    }
                                }

                            }
                            //if rsid isn't found or the next rsid isn't conclusive, 74% chance of brown and 21% green
                            return "Brown: 74%, Green: 21%, Blue: 5%";
                        }
                        //if heterozygous, 73% chance of green
                        else
                        {
                            return "Green: 73%, Brown or Blue: 13%";
                        }
                    
                    }
				    
				}
                //if TT, test next rsid
                else
                {
                    index = id.FindIndex(x => x.StartsWith("rs1393350"));
                    if (index > -1)
                    {
                        al1 = strand[index].allele1;
                        al2 = strand[index].allele2;
                        //if GG, test next rsid
                        if ((al1.Equals(al2) && al1.Equals("G")))
                        {
                            index = id.FindIndex(x => x.StartsWith("rs12896399"));
                            if(index > -1)
                            {
                                al1 = strand[index].allele1;
                                al2 = strand[index].allele2;
                                //if GG, 60% chance green
                                if(al1.Equals(al2) && al1.Equals("G"))
                                {
                                    return "Green 60%, Blue 40%";
                                }
                                //if AA or heterozygous, 73% chance blue
                                else
                                {
                                    return "Blue: 73%, Green: 27%";
                                }
                            }
                        }
                        //if TT or heterozygous, 95% chance blue
                        else
                        {
                            return "Blue: 95%, Green: 5%";
                        }
                    }
                }
            }
            //if all testing fails, return Unknown
            return "Unknown";
        }
    }
}
