using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    void Start()
    {
        Button loginBtn = GetControl<Button>("SignInBtn");
        if (loginBtn)
        {
            loginBtn.onClick.AddListener(() =>
            {
                Debug.Log("LoginBtnClick");
            });
        }

        Button logoutBtn = GetControl<Button>("LogOutBtn");
        if (logoutBtn)
        {
            logoutBtn.onClick.AddListener(() =>
            {
                Debug.Log("LogoutBtnClick");
            });
        }

        Text loginText = GetControl<Text>("SigInText");
        if (loginText)
        {
            loginText.text = "login";
        }

        Text logoutText = GetControl<Text>("LogOutText");
        if (logoutText)
        {
            logoutText.text = "logout";
        }
    }
}
