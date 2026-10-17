# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-10-17

### Added

- `ToPascalCase`, `ToCamelCase`, and `ToSnakeCase` string extension methods.
  Conversions use word boundaries and invariant casing while preserving symbols.
- A strong-named `netstandard2.0` library using the shared organization key,
  with XML documentation, a portable PDB, and a `snupkg` symbol package.
