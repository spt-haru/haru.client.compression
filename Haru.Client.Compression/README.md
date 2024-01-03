# Haru.Client.Compression

Replaces EFT's `ComponentAce.Compressions.Libs.Zlib` calls to
`Zlib.Managed`.

## Why?

EFT's zlib library is:

- Bugged. Zero-tail in compressed data leads to infinite loop,
  `Haru.Encryption.AesBase` triggers this bug.
- Thread-unsafe. In the past it caused buffer overlap in async code, which
  resulted in corrupted data.
- Slower. `Zlib.Managed` comes with better memory pooling and uses optimized
  types for memory manipulation.

By replacing EFT's implementation of zlib with Haru's, the game should become
a littlebit faster and more stable.

## Notes

Only methods called by others are overriden. Some `SimpleZlib` methods are
unused within EFT's assembly.
