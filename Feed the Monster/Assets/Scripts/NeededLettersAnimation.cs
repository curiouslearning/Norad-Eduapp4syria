using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class NeededLettersAnimation  {

	Image mTarget;
	int mDefaultSize;
	int mPopSize;
	float mTimeStarted;
	float mLength;
	bool mRunning;
	int mLastIndex;
	float mLastTimeSine;

	MonsterCalloutController mController;


	public NeededLettersAnimation(Image target, MonsterCalloutController controller)
	{
		mTarget = target;
		mController = controller;
		//mDefaultSize = mTarget.fontSize;
		mPopSize = (int)(mDefaultSize * 1.25f);
	}

	public void Show(float length)
	{
		mTimeStarted = Time.time;
		mLastTimeSine = Time.time;
		mLength = length;
		Timer.Instance.Create (new Timer.TimerCommand (Time.time, 0.01f, Update));
		mLastIndex = 0;
		mRunning = true;
	}

	void Update()
	{
		float lerpRate = Mathf.Abs(Mathf.Sin((Time.time - mLastTimeSine) * 3f));

		if (mRunning) {
			SetSize ((int)((float)mDefaultSize * (1f - lerpRate) + (float)mPopSize * lerpRate));
			if (Time.time >= mTimeStarted + mLength) {
				SetSize (mDefaultSize);
				mRunning = false;
			}
		}
	}


    void loadCharImage(string CharName)
    {
        string url;
        Sprite res;
        CharName = CharName.Normalize(NormalizationForm.FormD);
        url = "charimg/" + CharName;
        res = Resources.Load<Sprite>(url);
        mTarget.sprite = res;
        mTarget.enabled = true;
        if (res == null)
        {
            Debug.Log("Can't load character image: " + url);
        }
    }

    void SetSize(int sizeForNeededLetters)
	{
		string richTextForUI = "";
		string letter;

		switch (GameplayController.Instance.CurrentLevel.monsterInputType) {
		case MonsterInputType.Letter:
//			letter = GameplayController.Instance.CurrentSegment.MonsterRequiredLetters [0];
			letter = GameplayController.Instance.CurrentSegment.GetFixRequiredLetters(0);
//			letter = ArabicSupport.ArabicFixer.Fix(letter, true, true);
//			letter = RTL.Fix(letter);

			richTextForUI += letter;
			break;
	

	
		}

        if (mTarget != null)
            loadCharImage(richTextForUI);
			//mTarget.text = richTextForUI;
	}
    

	
}
