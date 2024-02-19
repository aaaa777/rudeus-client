# APIリファレンス

アプリが利用するREST APIのリファレンスです。

ペイロード等の詳細なAPIリファレンスは[こちら](https://win.nomiss.net/admin/docs/api/eris)を参照してください。(ユーザ/パスワードはadmin/adminです。)

## POST /api/device_initialize

デバイスを初期化するためのAPIです。初期化後にアクセストークンが発行されます。

## POST /api/user_login

note: deprecated
廃止されたAPIです。

## POST /api/device_update

デバイスの情報を更新するためのAPIです。

## POST /api/device_mac_update

デバイスのMACアドレスを更新するためのAPIです。

## GET /api/update_metadata

アップデート情報を取得するためのAPIです。

## POST /api/check_access_token

未実装

アクセストークンの有効性を確認するためのAPIです。

## POST /api/application_update

未実装

## GET /api/check_server_status

未実装
