Prefixr [Notepad++ Plugin]
=========================

Prefixr for Netbeans is a plugin developed by **Joao Lopes** that
uses the website [http://prefixr.com/](http://prefixr.com/) created by **Jefrey Way**

![Screenshot](https://github.com/DPr00f/PrefixrNpp/raw/master/prefixr-screenshot.jpg)


Contributing
------------

Feel free to do whatever you want with the source code, but please give me some credits :)
If you want you can send me an email (mail **at** joaolopes.info).
Maybe i'll join the cause.


Installation
------------

1. Download plugin [here](https://github.com/downloads/DPr00f/PrefixrNpp/Prefixr.dll)
2. Close Notepad++ if opened
3. Locate the Notepad++ `plugins` folder. (Normally `%PROGRAMFILES(x86)%\Notepad++\plugins\` or `%PROGRAMFILES%\Notepad++\plugins\`)
4. Locate [Prefixr.dll](https://github.com/downloads/DPr00f/PrefixrNpp/Prefixr.dll) file and copy it to the `plugins` folder.
5. Start Notepad++. 


Usage
-----

Select the css that you want to Prefix and press `Ctrl+Alt+P`
**Selecting nothing will cause all css to be prefixed.**

    div{ border-radius:30px }
    returns
    div{ -webkit-border-radius: 30px; -khtml-border-radius: 30px; -moz-border-radius: 30px; border-radius: 30px; }