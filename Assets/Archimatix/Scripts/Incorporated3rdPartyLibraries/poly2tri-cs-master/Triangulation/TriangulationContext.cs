﻿/* AXPoly2Tri
 * Copyright (c) 2009-2010, AXPoly2Tri Contributors
 * http://code.google.com/p/poly2tri/
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 *
 * * Redistributions of source code must retain the above copyright notice,
 *   this list of conditions and the following disclaimer.
 * * Redistributions in binary form must reproduce the above copyright notice,
 *   this list of conditions and the following disclaimer in the documentation
 *   and/or other materials provided with the distribution.
 * * Neither the name of AXPoly2Tri nor the names of its contributors may be
 *   used to endorse or promote products derived from this software without specific
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System.Collections.Generic;

namespace AXPoly2Tri {
	public abstract class TriangulationContext {
		public TriangulationDebugContext DebugContext { get; protected set; }

		public readonly List<DelaunayTriangle> Triangles = new List<DelaunayTriangle>();
		public readonly List<TriangulationPoint> Points = new List<TriangulationPoint>(200);
		public TriangulationMode TriangulationMode { get; protected set; }
		public Triangulatable Triangulatable { get; private set; }

		public int StepCount { get; private set; }

		public void Done() {
			StepCount++;
		}

		public abstract TriangulationAlgorithm Algorithm { get; }

		public virtual void PrepareTriangulation(Triangulatable t) {
			Triangulatable = t;
			TriangulationMode = t.TriangulationMode;
			t.Prepare(this);
		}

		public abstract TriangulationConstraint NewConstraint(TriangulationPoint a, TriangulationPoint b);

		public void Update(string message) {}

		public virtual void Clear() {
			Points.Clear();
			if (DebugContext != null) DebugContext.Clear();
			StepCount = 0;
		}

		public virtual bool IsDebugEnabled { get; protected set; }

		public DTSweepDebugContext DTDebugContext { get { return DebugContext as DTSweepDebugContext; } }
	}
}