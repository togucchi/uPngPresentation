# uPngPresentation

## 概要
Unityで連番PNGファイルを読み込んでプレゼンソフトのように遷移できるようにするスクリプトです．
## 使い方

### PNGを書き出す
Keynote，PowerPointなどのソフトで作ったスライドを連番PNGファイルで書き出します．

### Unityに取り込む
PNGファイルをStreamingAssetsの中に入れる．

### uPngPresentationをアタッチ
GameObjectにuPngPresentationをアタッチし，描画対象のRenderTextureやStreamingAssetsの中のフォルダ名を設定する．

### RenderTextureを自由に処理
設定したRenderTextureにスライドが描画されるので，自由に使う．（サンプルシーン参照）
