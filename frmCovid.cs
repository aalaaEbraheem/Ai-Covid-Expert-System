﻿using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace CoronaDiagnoser
{
    public partial class frmCovid : XtraForm
    {
        public frmCovid()
        {
            InitializeComponent();
        }
        // Point values.
        private const int Common = 20;
        private const int Sometimes = 5;
        private const int Mild = 3;
        private const int Rare = 1;
        private const int No = 0;

        // Values for each diagnosis.
        private int[,] Values =
        {
            { Common, Common, Common, Sometimes, Sometimes, Sometimes, Sometimes, Rare, Rare, No },
            { Rare, Mild, No, Rare, Common, Common, Sometimes, No, Common, Common },
            { Common, Common, No, Common, Common, Common, Common, Sometimes, No, No },
            { Sometimes, Sometimes, Common, Sometimes, No, No, Sometimes, No, Common, Common },
        };
        private const int DiarrheaSymptom = 7;

        // Indices in the array.
        private int Corona = 0;
        private int CommonCold = 1;
        private int Flu = 2;
        private int Allergies = 3;

        // An array of CheckBoxes so we can loop over them.
        private CheckEdit[] CheckBoxes;
        private void frmCovid_Load(object sender, EventArgs e)
        {
            // Load the CheckBoxes array.
            CheckBoxes = new CheckEdit[]
            {
                chkFever,
                chkDryCough,
                chkShortnessOfBreath,
                chkHeadaches,
                chkAchesAndPains,
                chkSoreThroat,
                chkFatigue,
                chkDiarrhea,
                chkRunnyNose,
                chkSneezing,
            };
            Analyze();
        }
        private void Analyze()
        {
            int num_diagnoses = Values.GetUpperBound(0) + 1;
            int num_symptoms = Values.GetUpperBound(1) + 1;
            int[] totals = new int[num_diagnoses];
            for (int diagnosis = 0; diagnosis < num_diagnoses; diagnosis++)
            {
                for (int symptom = 0; symptom < num_symptoms; symptom++)
                {
                    if (CheckBoxes[symptom].Checked)
                        totals[diagnosis] += Values[diagnosis, symptom];
                }
            }
            // If an adult, remove diarrhea from flu.
            if (chkAdult.Checked && chkDiarrhea.Checked)
            {
                totals[Flu] -= Values[Flu, DiarrheaSymptom];
            }
            // Display results.
            Label[] labels = { lblCoronaVirus, lblCold, lblFlu, lblAllergies };
            for (int diagnosis = 0; diagnosis < num_diagnoses; diagnosis++)
            {
                labels[diagnosis].Width = totals[diagnosis];
                labels[diagnosis].Text = totals[diagnosis].ToString();
            }
        }
        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (CheckEdit chk in CheckBoxes)
            {
                chk.Checked = false;
            }
        }
        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (CheckEdit chk in CheckBoxes)
            {
                chk.Checked = true;
            }
        }
        private void chkSymptom_CheckedChanged(object sender, EventArgs e)
        {
            Analyze();
        }
    }
}
