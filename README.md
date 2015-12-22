# Edm
Edmは、主に下記の2つのプロジェクトから構成されます。

* Excelをモデル化したデータ構造を表すEdm
* EPPlusを使ってEdmからExcel形式のファイルに変換するEdm.Writer.EPPlus

Edmで提供されるモデルを構築すれば、具体的なExcel出力のためのコードを書かなくてもExcel形式のファイルが出力できます。

通常は、上位プロジェクトであるSdmと、Sdm/Edmに依存したRealizer.Edmを使って、
SdmからExcel形式のファイルを出力する形で使うことになります。

## プロジェクトの構造
### Edm
Edmは他のプロジェクトに全く依存していない、Excelのモデルを提供するためだけのプロジェクトです。

### Edm.Writer.EPPlus
Edmでは、実際にExcel形式のファイルを出力するコンポーネントをWriterと呼び、
`Edm.Writer.<出力に使うコンポーネント名>`というプロジェクトに配置します。
現状、EPPlusにしか対応していませんが、WriterはEdmと出力に使うコンポーネントにしか依存しないため、
ユーザーが独自に実装もできるようになっています。

## メンテナ
- [@bleis-tift](https://github.com/bleis-tift)

