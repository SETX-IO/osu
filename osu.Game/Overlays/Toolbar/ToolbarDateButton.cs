// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Testing;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens;
using osu.Game.Screens.GameBox;
using osu.Game.Screens.Menu;
using osuTK;

namespace osu.Game.Overlays.Toolbar
{
    public partial class ToolbarDateButton : OsuClickableContainer
    {
        private Box hoverBackground;
        private OsuSpriteText text;
        private OsuGame game;

        public ToolbarDateButton()
        {
            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;
        }

        [BackgroundDependencyLoader(true)]
        private void load(OsuGame game)
        {
            this.game = game;
            Padding = new MarginPadding(3);

            Children = new List<Drawable>
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    CornerRadius = 6,
                    CornerExponent = 3f,
                    Children = new Drawable[]
                    {
                        hoverBackground = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = OsuColour.Gray(80).Opacity(180),
                            Blending = BlendingParameters.Additive,
                            Alpha = 0,
                        },
                    }
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(5),
                    Padding = new MarginPadding(10),
                    Children = new Drawable[]
                    {
                        text = new OsuSpriteText
                        {
                            Text = "Game",
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                        },
                    }
                }
            };
        }

        protected override bool OnClick(ClickEvent e)
        {
            try
            {
                var screenStack = game.ChildrenOfType<OsuScreenStack>().FirstOrDefault();
                if (!(screenStack.CurrentScreen is MainMenu))
                    return base.OnClick(e);

                screenStack.Push(new GameBoxMainScreen());
            }
            catch
            {
            }

            return base.OnClick(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            hoverBackground.FadeIn(200);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            hoverBackground.FadeOut(200);

            base.OnHoverLost(e);
        }
    }
}
