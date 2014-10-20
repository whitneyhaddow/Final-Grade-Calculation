using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_4

/***************************************************
 * This form calculates final grade for a student
 * based on grades from various class components
 * Created October 2014 by Whitney Haddow
 ****************************************************/
{
    public partial class Form1 : Form
    {
        //declare constants
        const decimal QUIZ_CONTRIBUTION = 0.30m;
        const decimal MIDTERM_CONTRIBUTION = 0.30m;
        const decimal FINAL_CONTRIBUTION = 0.40m;
   
        //declare an array
        const int MAX = 10;
        decimal[] quizzes = new decimal[MAX];
        int count = 0; //number of quiz grades entered, 0 is default


        public Form1()
        {
            InitializeComponent();
      
        }

       //add quiz grades to the array
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidDecimalRange(txtQuiz, 100, "Quiz grade")) //is the input valid?
            {
                if (count < MAX) //more grades can still be added
                {
                    quizzes[count] = Decimal.Parse(txtQuiz.Text);
                    count++; //add one more number
                    txtQuiz.Text = ""; //clears the textbox to prepare for next entry
                    txtQuiz.Focus(); //puts focus on the text box

                    if (count == (MAX)) //last value is being added
                    {
                        txtMidterm.Focus(); //places focus on the midterm textbox
                        btnAdd.Enabled = false; //they cannot click the add button anymore
                        MessageBox.Show("The maximum amount of grades has now been entered.",
                            "Quiz Entry Complete");
                    }
                }
            }

           

            //display values from the array in the listbox
            lstQuizGrades.Items.Clear();

            for (int i = 0; i < count; i++)
            {
                lstQuizGrades.Items.Add(quizzes[i]);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (IsValidDecimalRange(txtMidterm, 100, "Midterm Grade") && IsValidDecimalRange(txtFinalExam, 100, "Final Exam Grade")) //are the inputs valid?
            {
                
                //declare variables
                decimal quizAverage; //average of quiz grades remaining after lowest dropped
                decimal midterm; //grade for midterm
                decimal finalExam; //grade for final exam
                decimal finalGrade; //final grade once all grades are compiled
                char finalLetter; //final letter grade corresponding to the final grade
                decimal quizPercent; //percent contribution of the average quiz grade
                decimal midtermPercent; //percent contribution of midterm
                decimal finalExamPercent; //percent contribution of final exam

                //subtract lowest quiz and calculate average of remaining quiz grades
                decimal lowestValue = FindLowest(quizzes, count);
                decimal sum = 0.0m;
                for (int i = 0; i < quizzes.Length; i++)
                {
                    sum += quizzes[i];
                }
                quizAverage = (sum - lowestValue) / (count - 1);

                //get the percent value for quizAverage
                decimal percentAverage = quizAverage / 100;

                //get other inputs
            
                midterm = Convert.ToDecimal(txtMidterm.Text) / 100;
                finalExam = Convert.ToDecimal(txtFinalExam.Text) / 100;

                //calculate percent contribution for all grades
                quizPercent = percentAverage * QUIZ_CONTRIBUTION;
                midtermPercent = midterm * MIDTERM_CONTRIBUTION;
                finalExamPercent = finalExam * FINAL_CONTRIBUTION;

                //calculate final grade based on contributions
                finalGrade = (quizPercent + midtermPercent + finalExamPercent);

                //determine corresponding letter grade
                if (finalGrade >= 0.9m)
                    finalLetter = 'A';
                else if (finalGrade >= 0.8m && finalGrade < 0.9m)
                    finalLetter = 'B';
                else if (finalGrade >= 0.7m && finalGrade < 0.8m)
                    finalLetter = 'C';
                else if (finalGrade >= 0.6m && finalGrade < 0.7m)
                    finalLetter = 'D';
                else
                    finalLetter = 'F';

                //display results
                lblFinalGrade.Text = finalGrade.ToString("p0");
                lblFinalLetter.Text = Convert.ToString(finalLetter);
                lblQuizDropped.Text = "The quiz with grade " + lowestValue + "% was dropped!";

            }
        }




        //allows user to start over
        private void btnStart_Click(object sender, EventArgs e)
        {
            //clear textboxes and labels
            txtMidterm.Text = "";
            txtFinalExam.Text = "";
            lblFinalGrade.Text = "";
            lblFinalLetter.Text = "";
            lblQuizDropped.Text = "";

            //clear array/listbox
            count = 0;
            lstQuizGrades.Items.Clear();

            //enable add button again
            btnAdd.Enabled = true;
        }

        //terminate application
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       //method to find the lowest quiz grade
        public decimal FindLowest(decimal[] numbers, int howMany)
        {
            decimal lowest = numbers[0]; //assume first element is the lowest
            for (int i = 0; i < howMany; i++)
            {
                if(numbers[i] < lowest) //if there is a lower one than the first element
                {
                    lowest = numbers[i];
                }
            }
            return lowest;
        }


        //generic function to validate is a txt box a valid decimal number within specified range
        public static bool IsValidDecimalRange(TextBox inputBox, decimal maxNumber, string inputName)
        {
            bool result = true; //default
            decimal temp; //auxillary variable (temporary) for try parse
            if (!Decimal.TryParse(inputBox.Text, out temp)) //incorrect format
            {
                MessageBox.Show(inputName + " should be entered as a number.",
                    "Error");
                inputBox.Focus();
                result = false;
            }
            else if (temp < 0) //negative
            {
                MessageBox.Show(inputName + " must be above 0.",
                    "Error");
                inputBox.Focus();
                result = false;
            }
            else if (temp > maxNumber) //higher than maximum
            {
                MessageBox.Show(inputName + " must be below " + maxNumber + ".",
                    "Error");
                inputBox.Focus();
                result = false;
            }
            return result;
        }
    
     
    }
      
}
