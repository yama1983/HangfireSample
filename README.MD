# HangfireSample
Hanfrire のサンプルソリューションです。
## 構成
デモは以下のプロジェクト構成になっています。

- App
  - Redis にバッチ処理のキューを登録します。
- Jobs
  - バッチ処理のクラスとインターフェースです。
- Server
  - Hangfire の サーバーになります。
    キューをもとに、バッチ処理を実行します。
    また、ダッシュボードを提供します。

## 依存するミドルウェア
- Redis
  - キュー管理に使用しています。

Redis 以外にも、SQLServer や MySQL、 PostgreSQL などのRDBを使用することもできます。
