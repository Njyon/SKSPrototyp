using UnityEngine;

namespace UnityEditor.U2D.Sprites
{
    internal partial class SpriteFrameModule : SpriteFrameModuleBase
    {
        private static class SpriteFrameModuleStyles
        {
            public static readonly GUIContent sliceButtonLabel = EditorGUIUtility.TrTextContentWithIcon("Slice", "Sprite creation and deletion is enabled.", "IN LockButton");
            public static readonly GUIContent sliceButtonLockLabel = EditorGUIUtility.TrTextContentWithIcon("Slice","Sprite creation and deletion is disabled.", "IN LockButton on");
            public static readonly GUIContent trimButtonLabel = EditorGUIUtility.TrTextContent("Trim", "Trims selected rectangle (T)");
            public static readonly GUIContent trimButtonLabelDisabled = EditorGUIUtility.TrTextContent("Trim", "Trims selected rectangle (T). Disabled because the Sprite Position field is locked.");
        }

        // overrides for SpriteFrameModuleBase
        public override void DoMainGUI()
        {
            // Do nothing when extension is activated.
            if (m_CurrentMode != null)
            {
                m_CurrentMode.DoMainGUI();
                return;
            }
            base.DoMainGUI();
            DrawSpriteRectGizmos();
            DrawPotentialSpriteRectGizmos();

            if (!spriteEditor.editingDisabled)
            {
                HandleGizmoMode();

                if (containsMultipleSprites)
                    HandleRectCornerScalingHandles();

                HandleBorderCornerScalingHandles();
                HandleBorderSidePointScalingSliders();

                if (containsMultipleSprites)
                    HandleRectSideScalingHandles();

                HandleBorderSideScalingHandles();
                HandlePivotHandle();

                if (containsMultipleSprites)
                    HandleDragging();

                spriteEditor.HandleSpriteSelection();

                if (containsMultipleSprites && m_CurrentEditEditCapability.data.HasCapability(EEditCapability.CreateAndDeleteSprite))
                {
                    HandleCreate();
                    HandleDelete();
                    HandleDuplicate();
                }
                spriteEditor.spriteRects = m_RectsCache.GetSpriteRects();
            }
        }

        private void DrawPotentialSpriteRectGizmos()
        {
            if (m_PotentialRects != null && m_PotentialRects.Count > 0)
                DrawRectGizmos(m_PotentialRects, Color.red);
        }

        public override void DoToolbarGUI(Rect toolbarRect)
        {
            using (new EditorGUI.DisabledScope(!containsMultipleSprites || spriteEditor.editingDisabled || m_TextureDataProvider.GetReadableTexture2D() == null || m_CurrentMode != null))
            {
                GUIStyle skin = EditorStyles.toolbarPopup;

                Rect drawArea = toolbarRect;

                var canCreateSprite = m_CurrentEditEditCapability.data.HasCapability(EEditCapability.CreateAndDeleteSprite);
                var sliceButtonStyle = canCreateSprite ? SpriteFrameModuleStyles.sliceButtonLabel : SpriteFrameModuleStyles.sliceButtonLockLabel;
                drawArea.width = skin.CalcSize(sliceButtonStyle).x;
                SpriteUtilityWindow.DrawToolBarWidget(ref drawArea, ref toolbarRect, (adjustedDrawArea) =>
                {
                    if(GUI.Button(adjustedDrawArea, sliceButtonStyle, EditorStyles.toolbarButton))
                    {
                        OnEditCapabilityChanged(EEditCapability.CreateAndDeleteSprite, !canCreateSprite);
                    }
                });

                drawArea.x += drawArea.width;
                drawArea.width = skin.CalcSize(new GUIContent("")).x;
                SpriteUtilityWindow.DrawToolBarWidget(ref drawArea, ref toolbarRect, (adjustedDrawArea) =>
                {
                    using (new EditorGUI.DisabledScope(!canCreateSprite))
                   {
                       if (GUI.Button(adjustedDrawArea, "", skin))
                       {
                           if (SpriteEditorMenu.ShowAtPosition(adjustedDrawArea, this, this))
                               GUIUtility.ExitGUI();
                       }
                   }
                });

                var canEditPosition = m_CurrentEditEditCapability.data.HasCapability(EEditCapability.EditSpriteRect);
                using (new EditorGUI.DisabledScope(!hasSelected || !canEditPosition))
                {
                    drawArea.x += drawArea.width;
                    drawArea.width = skin.CalcSize(SpriteFrameModuleStyles.trimButtonLabel).x;
                    SpriteUtilityWindow.DrawToolBarWidget(ref drawArea, ref toolbarRect, (adjustedDrawArea) =>
                    {
                        if (GUI.Button(adjustedDrawArea,
                                canEditPosition ? SpriteFrameModuleStyles.trimButtonLabel : SpriteFrameModuleStyles.trimButtonLabelDisabled,
                                EditorStyles.toolbarButton))
                        {
                            TrimAlpha();
                            Repaint();
                        }
                    });
                }
            }
        }

        private void HandleRectCornerScalingHandles()
        {
            if (!hasSelected)
                return;

            GUIStyle dragDot = styles.dragdot;
            GUIStyle dragDotActive = styles.dragdotactive;
            var color = Color.white;

            Rect rect = new Rect(selectedSpriteRect_Rect);

            float left = rect.xMin;
            float right = rect.xMax;
            float top = rect.yMax;
            float bottom = rect.yMin;

            EditorGUI.BeginChangeCheck();

            bool canEdit = !m_PositionToggleLock.value;
            HandleBorderPointSlider(ref left, ref top,  canEdit ?  MouseCursor.ResizeUpLeft : MouseCursor.NotAllowed, false, dragDot, dragDotActive, color);
            HandleBorderPointSlider(ref right, ref top, canEdit ? MouseCursor.ResizeUpRight : MouseCursor.NotAllowed, false, dragDot, dragDotActive, color);
            HandleBorderPointSlider(ref left, ref bottom, canEdit ? MouseCursor.ResizeUpRight : MouseCursor.NotAllowed, false, dragDot, dragDotActive, color);
            HandleBorderPointSlider(ref right, ref bottom, canEdit ? MouseCursor.ResizeUpLeft : MouseCursor.NotAllowed, false, dragDot, dragDotActive, color);

            if (EditorGUI.EndChangeCheck() && canEdit)
            {
                rect.xMin = left;
                rect.xMax = right;
                rect.yMax = top;
                rect.yMin = bottom;
                ScaleSpriteRect(rect);
                PopulateSpriteFrameInspectorField();
            }
        }

        private void HandleRectSideScalingHandles()
        {
            if (!hasSelected)
                return;

            Rect rect = new Rect(selectedSpriteRect_Rect);

            float left = rect.xMin;
            float right = rect.xMax;
            float top = rect.yMax;
            float bottom = rect.yMin;

            Vector2 screenRectTopLeft = Handles.matrix.MultiplyPoint(new Vector3(rect.xMin, rect.yMin));
            Vector2 screenRectBottomRight = Handles.matrix.MultiplyPoint(new Vector3(rect.xMax, rect.yMax));

            float screenRectWidth = Mathf.Abs(screenRectBottomRight.x - screenRectTopLeft.x);
            float screenRectHeight = Mathf.Abs(screenRectBottomRight.y - screenRectTopLeft.y);

            EditorGUI.BeginChangeCheck();

            bool canEdit = !m_PositionToggleLock.value;
            left = HandleBorderScaleSlider(left, rect.yMax, screenRectWidth, screenRectHeight, true, canEdit);
            right = HandleBorderScaleSlider(right, rect.yMax, screenRectWidth, screenRectHeight, true, canEdit);

            top = HandleBorderScaleSlider(rect.xMin, top, screenRectWidth, screenRectHeight, false, canEdit);
            bottom = HandleBorderScaleSlider(rect.xMin, bottom, screenRectWidth, screenRectHeight, false, canEdit);

            if (EditorGUI.EndChangeCheck() && canEdit)
            {
                rect.xMin = left;
                rect.xMax = right;
                rect.yMax = top;
                rect.yMin = bottom;

                ScaleSpriteRect(rect);
                PopulateSpriteFrameInspectorField();
            }
        }

        private void HandleDragging()
        {
            if (hasSelected && !MouseOnTopOfInspector() && !m_PositionToggleLock.value)
            {
                Rect textureBounds = new Rect(0, 0, textureActualWidth, textureActualHeight);
                EditorGUI.BeginChangeCheck();

                Rect oldRect = selectedSpriteRect_Rect;
                Rect newRect = SpriteEditorUtility.ClampedRect(SpriteEditorUtility.RoundedRect(SpriteEditorHandles.SliderRect(oldRect)), textureBounds, true);

                if (EditorGUI.EndChangeCheck())
                {
                    selectedSpriteRect_Rect = newRect;
                    UpdatePositionField(null);
                }
            }
        }

        private void HandleCreate()
        {
            if (!MouseOnTopOfInspector() && !eventSystem.current.alt)
            {
                // Create new rects via dragging in empty space
                EditorGUI.BeginChangeCheck();
                Rect newRect = SpriteEditorHandles.RectCreator(textureActualWidth, textureActualHeight, styles.createRect);
                if (EditorGUI.EndChangeCheck() && newRect.width > 0f && newRect.height > 0f)
                {
                    CreateSprite(newRect);
                    GUIUtility.keyboardControl = 0;
                }
            }
        }

        private void HandleDuplicate()
        {
            IEvent evt = eventSystem.current;
            if ((evt.type == EventType.ValidateCommand || evt.type == EventType.ExecuteCommand)
                && evt.commandName == EventCommandNames.Duplicate)
            {
                if (evt.type == EventType.ExecuteCommand)
                    DuplicateSprite();

                evt.Use();
            }
        }

        private void HandleDelete()
        {
            IEvent evt = eventSystem.current;

            if ((evt.type == EventType.ValidateCommand || evt.type == EventType.ExecuteCommand)
                && (evt.commandName == EventCommandNames.SoftDelete || evt.commandName == EventCommandNames.Delete))
            {
                if (evt.type == EventType.ExecuteCommand && hasSelected)
                    DeleteSprite();

                evt.Use();
            }
        }

        public override void DoPostGUI()
        {
            if (m_CurrentMode != null)
                m_CurrentMode.DoPostGUI();
            else
                base.DoPostGUI();
        }
    }
}
