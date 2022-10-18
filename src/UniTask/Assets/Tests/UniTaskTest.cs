using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

public class UniTaskTest : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < 5; i++)
			WaitUpForget(i).Forget();
		for (int i = 0; i < 5; i++)
			WaitUpForEach(i);
	}
	public async UniTaskVoid WaitUpForget(int i)
	{
		while (true)
		{
			await up.WaitAsync();
			Test(i, "Forget");
		}
	}
	public void WaitUpForEach(int i)
	{
		up.ForEachAsync(_ => Test(i, "ForEach"));
	}
	public void Test(int i, string pre)
	{
		Debug.Log($"{pre} [{i}]");
	}
	AsyncEvent up = new AsyncEvent();
	void Update()
	{
		if (up != null)
			if (Time.frameCount % 300 == 299)
			{
				up.Dispose();
			}
			else
			if (Time.frameCount % 10 == 1)
			{
				up.Invoke();
			}
	}
}
