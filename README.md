# OmiLAXR.MetaXR

This package implemented `Actor Pipeline` components of the OmiLAXR framework supporting xAPI data format.

Just drag and drop the prefab `Resources/Prefab/OmiLAXR (MetaXR Variant).prefab` into your scene.

## Compatibility

Because of the less dependencies and modular design of OmiLAXRv2 this framework is compatible with all XR frameworks.

The only thing we need to consider are the Unity version. We try to support as much as possible upwards of Unity 2019.4.40f1. You are invited to contribute!

The compatibility was tested on

- [ ]  Unity 2019.4.41f1 (LTS)
- [X]  Unity 2020.3.49f1 (LTS)
- [X]  Unity 2021.3.45f2 (LTS)
- [X]  Unity 2022.3.62ff (LTS)
- [X]  Unity 2023.1.22f1 
- [X]  Unity 6000.0.58f2 (LTS)

Please contact us on https://discord.com/invite/u4gtXTBEx3 if it is not running on your Unity version. We will try to make it possible.

## Install by using scoped registry
1. Ensure in "Project settings" > "Package Manager" that you have the scoped registry with following settings:
- Name: npmjs
- URL: http://registry.npmjs.com
- Scope(s): `com.rwth.unity.omilaxr.metaxr`
2. Go to Package Manager.
3. Click on the (+) button.
4. Select 'Add package by name'.
5. Place in 'Name' field: `com.rwth.unity.omilaxr.metaxr`.

### Adding scoped registry by using manifest.json (also recommended - quick way)
1. Alternatively, instead of adding the scoped registry inside Unity editor you can do it by using `manifest.json` file.
2. Go to you project root and then open `Packages/manifest.json`.
3. Ensure following entries in your file: `"scopedRegistries": [
   {
   "name": "npmjs",
   "url": "http://registry.npmjs.com/",
   "scopes": [
   "com.rwth.unity.omilaxr"
   ]
   }]`.
4. Go to the Unity Package Manager to `My registries` and install the package `com.rwth.unity.omilaxr.metaxr`.

## Install by using Git url
1. Go to Package Manager.
2. Click on the (+) button.
3. Select 'Add package from git URL'.
4. Paste `https://github.com/SGoerzen/OmiLAXR.MetaXR.git` and confirm.

## Publication

You can publish your package at any npm registry.
It makes sense to publish packages for easier distribution in other projects.
But we recommend to use `npmjs.com`. [Here](https://docs.npmjs.com/creating-and-publishing-scoped-public-packages) you can get more details.
But the steps are very easy.

1. Create an account on `npmjs.com`.
2. On demand increase your `version` number in `package.json`.
3. Commit and push your changes.
4. Open a terminal.
5. Go to the root of your project.
6. Run `npm login` and login via browser (or what else you like).
7. Run `npm publish --access public`.
8. Wait until publication is ready.
