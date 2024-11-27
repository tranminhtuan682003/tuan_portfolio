// Assets/Tests/PlayMode/MyPlayModeTest.cs
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class MyPlayModeTest : MonoBehaviour
{
    [UnityTest]
    public IEnumerator GameObjectShouldMoveToPositionOneOneOne()
    {
        var gameObject = new GameObject();
        yield return null; // Wait for one frame

        // Act
        gameObject.transform.position = new Vector3(1, 1, 1);

        // Assert
        Assert.AreEqual(new Vector3(1, 1, 1), gameObject.transform.position);
    }
}
