# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [2.2.10-preview.5] - 2019-08-29
### Bugfixes
- Storyboard: added global mute function
- Added ApplyBeforeBody option to POV component, to support working with FramingTransposer
- Added RectenterTarget to POV component
- Added OnTransitionFromCamera callback to extensions
- Added Speed Mode to AxisState, to support direct axis control without max speed
- New example scene: OverTheShoulderAim illustrating how to do over-the-shoulder TPS cam, with Normal and Aim modes
- Impulse Manager: added option to ignore timescale
- Framing Transposer: added OnTransition handling for camera rotation if InheritPosition
- Removed Resources diretories
- Regression Fix: Framing Transposer: ignore LookAt target.  Use Follow exclusively
- Bugfix: PostProcessing extension was not handling standby update correctly when on Manager Vcams
- Bugfix: added EmbeddedAssetHelper null check
- Bugfix: composer screen guides drawn in correct place for physical camera
- Bugfix: Storyboard canvases were not always destructed properly
- Bugfix: FreeLook was not respecting wait time for X axis recentering
- Bugfix: FreeLook X axis was not always perfectly synched between rigs
- Bugfix (fogbugz 1176866): Collider: clean up static RigidBody on exit
- Bugfix (fogbugz 1174180): framing transposer wrong ortho size calculation
- Bugfix (fogbugz 1158509): Split brain.UpdateMethod into VcamUpdateMethod and BrainUpdateMethod, to make blending work correctly
- Bugfix (fogbugz 1162074): Framing transposer and group transposer only reached half maximum ortho size 
- Bugfix (fogbugz 1165599): Transposer: fix gimbal lock issue in LockToTargetWithWorldUp
- Bugfix: use vcam's up when drawing gizmos (orbital transposer and free look)


## [2.2.9] - 2019-05-22
### Mostly Bugfixes
- Added CinemachineCore.GetBlendOverride delegate to allow applications to override any vcam blend when it happens
- When a blend is cancelled by the opposite blend, reduce the blend time
- FreeLook: if inherit position from similar FreeLooks, bypass damping 
- Timeline: improve handling when vcam values are tweaked inside shot inspector (fogbugz 1109024)
- OnTargetObjectWarped no longer generates garbage
- Orthographic cameras allow a Near Clip of 0
- Timeline won't auto-create CM brains when something dragged onto it
- Confiner: Improvement in automatic path invalidation when number of path points path changes
- Added CinemachineInpuitAxisDriver utility for overriding the default AxisState behaviour
- CinemachineCameraOffset: added customizable stage for when to apply the offset
- Added Loop option to BlendList Camera
- Improved Lookahed: does not automatically recenter
- Bugfix: potential endless loop when using Ignore tag in Collider
- Brain no longer applies time scaling to fixed delta
- Bugfix: Allow externally-driven FeeLook XAxis to work properly with SimpleFollow
- Bugfix: vcams with noise would sometimes show one noiseless frame when they were activated and standby update was not Always
- Bugfix: Generate a cut event if cutting to a blend-in-progess (fogbugz 1150847)
- Bugfix: reset lens shift if not physical camera
- Bugfix: Collider must consider actual target position, not lookahead position
- Bugfix: FreeLook heading RecenterNow was not working
- Bugfix: lookahead now takes the overridden Up into account
- Bugfix: screen composer guides drawn in wrong place for picture-in-picture
- Bugfix: FreeLook now draws only 1 active composer guide at a time (fogbugz 1138263)
- Bugfix: cameras sometimes snapped when interrupting blends
- Bugfix: Path handles no longer scale with the path object
- Bugfix: Framing Transposer Center on Activate was not working properly (fogbugz 1129824)
- Bugfix: collider was pushing camera too far if there were multiple overlapping obstacles
- Bugfix: use IsAssignableFrom instead of IsSubclass in several places
- Bugfix: when interrupting a blend in progress, Cut was not respected
- Bugfix: collider minimum occlusion time and smoothing time interaction
- Bugfix: TargetGroup.RemoveMember error (fogbugz 1119028)
- Bugfix: TargetGroup member lerping jerk when member weight near 0
- Bugfix: Transposer angular damping should be 0 only if binding mode not LockToTarget

## [2.2.8] - 2018-12-10
### Bugfixes, optimizations, and some experimental stuff
- Transposer: added Angular Damping Mode, to support quaternion calculations in gimbal-lock situations
- Framing Transposer and Group Transposer: group composing bugfixes, respect min/max limits
- Added ConemachineCameraOffset extension, to offset the camera a fixed distance at the end of the pipeline
- Dolly Cart: added support for LateUpdate
- State-driven-camera: added [NoSaveDuringPlay] to Animated Target and Layer Index
- Added AxisState.Recentering.RecenterNow() API call to skip wait time and start recentering now (if enabled)
- Added NoLens blend hint, to leave camera Lens settings alone
- Added Composer.CenterOnActivate option, to disable automatic centering in dead zone when vcam is activated
- Updated documentation (corrections, and relocation to prevent importing)
- Upgrade: added support for nested prefabs in Unity 2018.3 (fogbugz 1077395)
- Optimization: position predictor is more efficient
- Optimization: Composer caches some calculations 
- Optimization: Fix editor slowdown when Lens Presets asset is missing
- Experimental: Optional new damping algorithm: attempt to reduce sensitivity to variable framerate
- Experimental: Optional new extra-efficient versions of vcam and FreeLook (not back-compatible)
- Timeline: play/pause doesn't kick out the timeline vcam
- Path editor: make sure game view gets updated when a path waypoint is dragged in the scene view
- Composer guides are shown even if Camera is attached to a renderTexture
- Bugfix: allow impulse definition to be a non-public field (property drawer was complaining)
- Bugfix: added null check for when there is no active virtual camera
- Bugfix: CollisionImpulseSource typo in detection of 2D collider
- Bugfix: PasteComponentValues to prefab vcams and FreeLooks were corrupting scene and prefabs
- Bugfix: Timeline mixer was glitching for single frames at the end of blends
- Bugfix: Added OnTransitionFromCamera() to POV and OrbitalTransposer, to transition axes intelligently
- Regression fix: if no active vcam, don't set the Camera's transform

## [2.2.7] - 2018-07-24
### Mostly bugfixes
- Bugfix: fogbugz case 1053595: Cinemachine Collider leaves hidden collider at origin that interferes with scene objects
- Bugfix: fogbugz case 1063754: empty target group produces console messages
- Bugfix: FreeLook Paste Component Values now pastes the CM subcomponents as well
- Bugfix: added extra null checks to support cases where current vcam is dynamically deleted
- Bugfix: reset BlendList when enabled
- Regression fix: FreeLook axis values get transferred when similar vcams transition
- Bugfix: cutting to BlendList vcam sometimes produced a few bad frames
- Bugfix: smart update tracks the targets more efficiently and correctly, and supports RigidBody interpolation (2018.2 and up)
- Enhancement: POV component interprets POV as relative to parent transform if there is one
- API change: OnCameraLive and CameraActivated events take outgoing vcam also as parameter (may be null)

## [2.2.0] - 2018-06-18
### Impulse Module and More
- New Cinemachine Impulse module for event-driven camera shakes
- New Event Helper script CinemachineTriggerAction takes action on Collider and Collider2D enter/exit events, and exposes them as UnityEvents
- New performance-tuning feature: Standby Update.  Controls how often to update the vcam when it's in Standby.  
- New NoiseSettings editor with signal preview
- Added Focal Length or Named FOV presets for Camera Lens
- Added support for Physical Camera: focal length and Lens Offset
- New improved Group framing algorithm: tighter group framing in GroupComposer and FramingTransposer
- Collider: now returns TargetIsObscured if the target is offscreen (great for cameras with fixed aim)
- Collider: added Minimum Occlusion Time setting, to ignore fleeting obstructions
- Collider: added Transparent Layers mask, to specify solid objects that don't obstruct view
- Collider: damping will no longer take the camera through obstacles
- Collider: Added separate damping setting for when target is being occluded vs when camera is being returned to its normal position
- Collider: added Smoothing setting, to reduce camera jumpiness in environements with lots of obstacles
- NoiseSettings: added checkbox for pure sine-wave instead of Perlin wave
- If no LookAt target, PostProcessing FocusTracksTarget offset is relative to camera
- TrackedDolly: Default up mode sets Up to World Up
- Virtual Camera: New Transitions section in inspector that gives more control over blending:
  - Blend Hint provides some control over how the position and rotation are interpolated
  - Inherit Position checkbox to ensure smooth positional handoff from outgoing camera
  - OnCameraLive event gets fired when the camera activates.  Useful for custom handlers.
- Added ScreenSpaceAimWhenTargetsDiffer as a vcam blend hint.  This influences what happens when blending between vcams with different LookAt targets
- Increased stability of vcams with very small FOVs
- Framing Transposer no longer requires LookAt to be null
- LensSettings Aspect, Orthographic, IsPhysicalCamera, SensorSize properties no longer internal
- Noise Profiles: don't magically create assets.  Prompt user for filename and location of new or cloned profiles
- Refactored interaction between timeline and CM brain, to improve handling of edge cases (fogbugz case 1048497)
- Bugfix: StateDrivenCamera Editor was not finding states if target was OverrideController
- Bugfix when dragging orbital transposer transform: take bias into account
- Bugfix: SaveDuringPlay was not handling asset fields correctly - was sometimes crushing assets
- Bugfix: SimpleFollow transposers were not initilizing their position correctly at game start
- Bugfix: Timeline with CM shot was causing jitter in some FixedUpdate situations
- Bugfix: Multiple brains with heterogeneous update methods were not behaving correctly.  CM will now support this, but you must make sure that the brains have different layer masks.
- Example scenes now include use of CinemachineTriggerAction script.  

## [2.1.13] - 2018-05-09
### Removed dependency on nonexistant Timeline package, minor bugfixes
- Bugfix: Custom Blends "Any to Any" was not working (regression)
- Bugfix: Composer was sometimes getting wrong aspect if multiple brains with different aspect ratios
- Bugfix: could not drag vcam transforms if multiple inspectors and one is hidden
- Bugfix: Framing Transposer initializes in the wrong place - noticeable if dead zone

## [2.1.12] - 2018-02-26
### Storyboard, Bugfixes and other enhancements.  Also some restructuring for Package Manager
- Project restructure: Removed Base, Timeline, and PostFX folders from project root.  PostProcessing code must now be manually imported from Cinemachine menu.  No more dependencies on scripting defines.
- New Storyboard extension, to display images over the vcams.  Comes with a Waveform monitor window for color grading
- New option to specify vcam position blend style: linear, spherical, or cylindrical, based on LookAt target
- Added API to support seamless position warping of target objects: OnTargetObjectWarped().
- Added support for custom blend curves
- Lookahead: added Ignore Y Axis Movement option
- Added support for cascading blends (i.e. blending from mid-blend looks better)
- POV/Orbital/FreeLook axis: exposed Min, Max, and Wrap in the UI, for customized axis range
- FreeLook: added Y Axis recentering
- POV: Added recentering feature to both axes
- Path: Added Normalized Path units option: 0 is start of path, 1 is end.
- Path: added length display in inspector
- Timeline Clip Editor: vcam sections are now collapsible
- API enhancement: added Finalize to Pipeline stages, called even for manager-style vcams
- Bugfix: PostProcessing V2 DoF blending works better
- Bugfix: OrbitalTransposer works better with WorldUp overrides
- Bugfix: Remove StateDrivenCamera "not playing a controller" warning
- Bugfix: Handle exceptions thrown by assemblies that don't want to be introspected
- Bugfix: vcams following physics objects incorrectly snapped to origin after exiting play mode
- Bugfix: predictor now supports time pause
- Bugfix: Moved StartCoroutine in Brain to OnEnable()
- Bugfix: Collider was causing problems in Physics on Android platforms
- Bugfix: dragging a vcam's position updtaes prefabs properly
- Bugfix: All extension now respect the "enabled" checkbox
- Bugfix: Undo for Extasion add will no longer generate null references

## [2.1.10] - 2017-11-28
### This is the first UPM release of *Unity Package Cinemachine*.
- New Aim component: Same As Follow Target simply uses the same orientation as the Follow target
- Perlin Noise component: added inspector UI to clone or locate existing Noise profiles, and to create new ones
- Noise Presets were moved outside of the Examples folder
- Example Assets are now included as embedded package, not imported by default
- Bugfix: FreeLook with PositionDelta was not properly updating the heading
- Bugfix: Transitioning between FreeLooks simetimes caused a short camera freeze
- Bugfix: Added some null checks to FreeLook, to prevent error messages at build time

## [2.1.9] - 2017-11-17
### Initial version.
*Version 2.1.9 cloned from private development repository, corresponding to package released on the asset store*