using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModuleController {

    bool IsActive();

    void SetActive(bool active);

}
