using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeManager : MonoBehaviour
{
    [System.Serializable]
    public class Pipe
    {
        public GameObject pipeNormal;
        public List<WaterVisual> pipeWaterVariants = new List<WaterVisual>();
        public float[] validRotations;
    }

    [System.Serializable]
    public class WaterVisual
    {
        public GameObject waterObject;
        public float[] validRotations;
    }

    public List<Pipe> pipes;
    public float delayBetweenPipes = 0.5f;

    public void StartCheck()
    {
        StartCoroutine(CheckPipes());
    }

    IEnumerator CheckPipes()
    {
        foreach (Pipe pipe in pipes)
        {
            float currentRotation = Normalize(pipe.pipeNormal.transform.eulerAngles.z);
            WaterVisual matchedVisual = null;

            foreach (var visual in pipe.pipeWaterVariants)
                if (visual.waterObject != null)
                    visual.waterObject.SetActive(false);

            foreach (var visual in pipe.pipeWaterVariants)
            {
                if (IsRotationCorrect(currentRotation, visual.validRotations))
                {
                    matchedVisual = visual;
                    break;
                }
            }

            if (matchedVisual != null)
            {
                pipe.pipeNormal.SetActive(false);
                matchedVisual.waterObject.SetActive(true);
            }
            else if (IsRotationCorrect(currentRotation, pipe.validRotations))
            {
                pipe.pipeNormal.SetActive(false);
                if (pipe.pipeWaterVariants.Count > 0 && pipe.pipeWaterVariants[0].waterObject != null)
                    pipe.pipeWaterVariants[0].waterObject.SetActive(true);
            }
            else
            {
                Debug.Log($"❌ Țeava {pipe.pipeNormal.name} este INCORRECTĂ!");
                yield return StartCoroutine(ResetPipes());
                yield break;
            }

            yield return new WaitForSeconds(delayBetweenPipes);
        }

        Debug.Log("🎉 Toate țevile sunt corecte! Nivel complet!");

        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (unlockedLevel < currentLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel);
            PlayerPrefs.Save();
        }



        SceneManager.LoadScene(currentLevel + 1);


    }

    IEnumerator ResetPipes()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (Pipe pipe in pipes)
        {
            pipe.pipeNormal.SetActive(true);
            foreach (var visual in pipe.pipeWaterVariants)
                if (visual.waterObject != null)
                    visual.waterObject.SetActive(false);
        }
    }

    float Normalize(float angle)
    {
        angle %= 360;
        if (angle < 0) angle += 360;
        return angle;
    }

    bool IsRotationCorrect(float rotZ, float[] validRotations)
    {
        foreach (float valid in validRotations)
            if (Mathf.Abs(rotZ - valid) < 5f)
                return true;

        return false;
    }
}
