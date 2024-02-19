# REST APIについて

Rudeusクライアントは、REST APIを使用して管理サーバーと通信します。

このページでは、Rudeusクライアントが使用するREST APIについて説明します。

## デバイスの登録
![register](/images/flow-register.png)

デバイスを登録するためには、`/api/device_initialize`を使用します。

このAPIは、デバイスを初期化し、アクセストークンを発行します。

初期化後には、デバイスの情報を更新するためのAPIを使用します。

`/api/device_initialize`を除くすべてのAPIは、初期化後に発行されるアクセストークンを使用して認証されます。

## デバイス情報の更新
![update](/images/flow-reg-update.png)

デバイス情報を更新するためには、`/api/device_update`を使用します。

このAPIは、デバイスの情報を最新の状態にします。
