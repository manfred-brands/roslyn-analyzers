﻿' <auto-generated />
' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Threading.Tasks

Namespace Global.Roslyn.Utilities
    ''' <summary>
    ''' Indicates that a code element Is performance sensitive under a known scenario.
    ''' </summary>
    ''' <remarks>
    ''' <para>When applying this attribute, only explicitly set the values for properties specifically indicated by the
    ''' test/measurement technique described in the associated <see cref="Uri"/>.</para>
    ''' </remarks>
    <Conditional("EMIT_CODE_ANALYSIS_ATTRIBUTES")>
    <AttributeUsage(AttributeTargets.Constructor Or AttributeTargets.Method Or AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=True, Inherited:=False)>
    Friend NotInheritable Class PerformanceSensitiveAttribute
        Inherits Attribute

        Public Sub New(uri As String)
            Me.Uri = uri
        End Sub

        ''' <summary>
        ''' Gets the location where the original problem Is documented, likely with steps to reproduce the issue And/Or
        ''' validate performance related to a change in the method.
        ''' </summary>
        Public ReadOnly Property Uri As String

        ''' <summary>
        ''' Gets Or sets a description of the constraint imposed by the original performance issue.
        ''' </summary>
        ''' <remarks>
        ''' <para>Constraints are normally specified by other specific properties that allow automated validation of the
        ''' constraint. This property supports documenting constraints which cannot be described in terms of other
        ''' constraint properties.</para>
        ''' </remarks>
        Public Property Constraint As String

        ''' <summary>
        ''' Gets Or sets a value indicating whether captures are allowed.
        ''' </summary>
        Public Property AllowCaptures As Boolean

        ''' <summary>
        ''' Gets Or sets a value indicating whether implicit boxing of value types Is allowed.
        ''' </summary>
        Public Property AllowImplicitBoxing As Boolean

        ''' <summary>
        ''' Gets Or sets a value indicating whether enumeration of a generic <see cref="IEnumerable{T}"/> Is allowed.
        ''' </summary>
        Public Property AllowGenericEnumeration As Boolean

        ''' <summary>
        ''' Gets Or sets a value indicating whether locks are allowed.
        ''' </summary>
        Public Property AllowLocks As Boolean

        ''' <summary>
        ''' Gets Or sets a value indicating whether the asynchronous state machine typically completes synchronously.
        ''' </summary>
        ''' <remarks>
        ''' <para>When <see langword="true"/>, validation of this performance constraint typically involves analyzing
        ''' the method to ensure synchronous completion of the state machine does Not require the allocation of a
        ''' <see cref="Task"/>, either through caching the result Or by using <see cref="ValueTask{TResult}"/>.</para>
        ''' </remarks>
        Public Property OftenCompletesSynchronously As Boolean

        ''' <summary>
        ''' Gets Or sets a value indicating whether this Is an entry point to a parallel algorithm.
        ''' </summary>
        ''' <remarks>
        ''' <para>Parallelization APIs And algorithms, e.g. <c>Parallel.ForEach</c>, may be efficient for parallel entry
        ''' points (few direct calls but large amounts of iterative work), but are problematic when called inside the
        ''' iterations themselves. Performance-sensitive code should avoid the use of heavy parallelization APIs except
        ''' for known entry points to the parallel portion of code.</para>
        ''' </remarks>
        Public Property IsParallelEntry As Boolean
    End Class
End Namespace
