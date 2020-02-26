using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateConditionsIteratively : MonoBehaviour {

    public List<string> conditions = new List<string>();
    public Dropdown conditionDropdown;

    [HideInInspector]
    public string[] selectedOrder;

	// Use this for initialization
	void Start () {
        PermutateIntegersTest();
	}
	
    private void RotateRight(List<string> sequence, int count) {
        string tmp = sequence[count - 1];
        sequence.RemoveAt(count - 1);
        sequence.Insert(0, tmp);
    }

    private IEnumerable<IList> Permutate(List<string> sequence, int count) {
        if (count == 1) yield return sequence;
        else {
            for (int i = 0; i < count; i++)
            {
                foreach (var perm in Permutate(sequence, count - 1))
                    yield return perm;
                RotateRight(sequence, count);
            }
        }
    }

    private void PermutateIntegersTest()
    {
        List<string> _dropDownOptions = new List<string>();
        string _option;

        foreach (List<string> permu in Permutate(conditions, conditions.Count)) {
            string[] listStrings = permu.ToArray();
            _option = string.Join(" ", listStrings);
            _dropDownOptions.Add(_option);          
        }

        conditionDropdown.ClearOptions();
        conditionDropdown.AddOptions(_dropDownOptions);
    }

    public void SelectOrder() {
        //selectedOrder = //conditionOrder.text.Split(' ');
    }
}
