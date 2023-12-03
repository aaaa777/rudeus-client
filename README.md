# Rudeus


# Documentations

- [API Document](https://aaaa777.github.io/rudeus-client/)


# Requirements

- .NET Core 7


# API support

- [x] /api/device_initialize
- [x] /api/user_login
- [x] /api/device_update
- [ ] /api/device_mac_update
- [x] /api/update_metadata
- [x] /api/check_access_token
- [x] /api/application_update
- [x] /api/check_server_status

# Secrets to be added

- `Base64_Encoded_Pfx` - コードサイン用のPFXファイルをBase64エンコードしたもの
- `Pfx_Passphrase` - コードサイン用のPFXファイルのパスワードをBase64エンコードしたもの
- 


# Generate document command

`docfx DocFX\docfx.json`