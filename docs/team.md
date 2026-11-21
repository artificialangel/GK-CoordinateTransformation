# Transform Lab - Team Rules

- Before starting a task, read `README.md` (concept, tab ownership, task checklist, technical constraints). Ask the user which role they have if it is unclear: Thiya (tabs 1-3) or Jei (tabs 4-5).
- Stack is fixed by the project: Unity 3D, C#, built-in UGUI, old Input Manager. Do not swap or add packages or frameworks.
- Use only the Unity `Transform` API. Do not write manual matrix math (decided with the course brief).
- Respect the technical constraints in `README.md`:
  - There is no settable "global scale". Show `lossyScale` as read-only and label it "lossy scale".
  - `Transform.Translate()` defaults to `Space.Self`; pass `Space.World` for world space.
  - The gimbal lock demo must apply three separate X, Y, Z rotations manually in a fixed order, with the middle axis near 90 degrees, and compare against a quaternion. Setting `eulerAngles` alone will not show gimbal lock.
  - `eulerAngles` read back may differ from the values set while representing the same rotation.
  - Unity is left-handed, Y up. Switching handedness means mirroring one axis, not just relabeling.
- Code and identifiers in English; README, PR text, and on-screen definitions may be Indonesian.
- Never commit to `main`. Branch as `<type>/<area>-<desc>` (e.g. `feat/rotation-gimbal-demo`), Conventional Commits, PR with every "Cek sebelum merge" checklist item ticked (self-merge allowed).
- Stay inside your area (see `.github/CODEOWNERS`). Changes outside it should be reviewed by that owner.
- Commit `.meta` files together with their assets. Only one person edits a `.unity` scene at a time.
- If code or ideas come from a reference repo, credit the source and check its licence.
- Do not modify files or code that were not asked to be changed. Ask before destructive commands or edits to unrelated sections.
